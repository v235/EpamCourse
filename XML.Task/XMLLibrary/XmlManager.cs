using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using Data.Entity;
using XML.Task.DAL;
using XML.Task.Entity;

namespace XMLLibrary
{
    public class XmlManager
    {
        private readonly IRepository<Book> booksRepository;
        private readonly IRepository<Newspaper> newsPapersRepository;
        private readonly IRepository<Patent> patentsRepository;

        public XmlManager(IRepository<Book> booksRepository, IRepository<Newspaper> newsPapersRepository, IRepository<Patent> patentsRepository)
        {
            this.booksRepository = booksRepository;
            this.newsPapersRepository = newsPapersRepository;
            this.patentsRepository = patentsRepository;
        }

        public void WriteXmlFile(Stream fs, string nsString)
        {
            using (XmlWriter xmlWriter = XmlWriter.Create(fs,
                new XmlWriterSettings {Indent = true}))
            {
                xmlWriter.WriteStartElement("catalog", nsString);
                xmlWriter.WriteAttributeString("dounloadtime", DateTime.Today.ToString());
                xmlWriter.WriteAttributeString("BooksCount", booksRepository.GetData().Count().ToString());
                xmlWriter.WriteAttributeString("NewspapersCount", newsPapersRepository.GetData().Count().ToString());
                xmlWriter.WriteAttributeString("PatentCount", patentsRepository.GetData().Count().ToString());
                xmlWriter.Flush();
                foreach (var book in GetBooks())
                {
                    WriteBookToXml(XNamespace.Get(nsString), book).WriteTo(xmlWriter);                                                   
                    xmlWriter.Flush();
                }
                foreach (var newsPaper in GetNewsPapers())
                {
                    WriteNewsPaperToXml(XNamespace.Get(nsString), newsPaper).WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                foreach (var patent in GetPatents())
                {
                    WritePatentToXml(XNamespace.Get(nsString), patent).WriteTo(xmlWriter);
                    xmlWriter.Flush();
                }
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
            }
        }

        public IEnumerable<BaseEntity> ReadXMLFile(Stream fs)
        {
            var schema = GetXmlSchema(CreateValidXml());
            using (XmlReader reader = XmlReader.Create(fs))

            {
                reader.MoveToContent();

                while (reader.Read())
                {

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        XElement el = XNode.ReadFrom(reader) as XElement;
                        if (el != null)
                        {
                            switch (el.Name.LocalName)
                            {
                                case "book":
                                    yield return GetBookEntity(el); ;
                                    break;
                                case "newspaper":
                                    yield return GetNewspaperEntity(el);
                                    break;
                                case "patent":
                                    yield return GetPatentEntity(el);
                                    break;
                            }

                        }
                    }
                }
            }
        }
        private Stream CreateValidXml()
        {
            FileStream fs = new FileStream("validXml.xml",FileMode.OpenOrCreate);
            using (XmlWriter xmlWriter = XmlWriter.Create(fs,
                new XmlWriterSettings {Indent = true}))
            {
                Book book = new Book();
                Newspaper newsPaper = new Newspaper();
                Patent patent = new Patent();

                xmlWriter.WriteStartElement("catalog", "");
                WriteBookToXml(XNamespace.Get(""), book).WriteTo(xmlWriter);
                WriteNewsPaperToXml(XNamespace.Get(""), newsPaper).WriteTo(xmlWriter);
                WritePatentToXml(XNamespace.Get(""), patent).WriteTo(xmlWriter);
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                fs.Close();
            }
            return new FileStream("validXml.xml", FileMode.Open);
        }

        private XmlSchemaSet GetXmlSchema(Stream validXml)
        {
            StringBuilder sb =new StringBuilder();
            XmlSchemaInference infer = new XmlSchemaInference();
            XmlSchemaSet schemaSet =
                infer.InferSchema(new XmlTextReader(validXml));
            XmlWriter w = XmlWriter.Create(sb,new XmlWriterSettings { Indent = true});
            foreach (XmlSchema schema in schemaSet.Schemas())
            {
                schema.Write(w);
            }
            w.Close();
            validXml.Close();
            XmlSchemaSet schemas = new XmlSchemaSet();
            schemas.Add("", XmlReader.Create(new StringReader(sb.ToString())));
            return schemas;
        }
        private Book GetBookEntity(XElement el)
        {
            Book book = new Book();
            book.Name = el.Element(el.Name.Namespace + nameof(book.Name)).Value;
            book.Authors = el.Element(el.Name.Namespace + nameof(book.Authors)).Value;
            book.PublishCity = el.Element(el.Name.Namespace + nameof(book.PublishCity)).Value;
            book.PublishingHouse = el.Element(el.Name.Namespace + nameof(book.PublishingHouse)).Value;
            book.PublishYear = XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(book.PublishYear)).Value);
            book.PageCount = XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(book.PageCount)).Value);
            book.Comment = el.Element(el.Name.Namespace + nameof(book.Comment)).Value;
            book.ISBN = el.Element(el.Name.Namespace + nameof(book.ISBN)).Value;
            return book;
        }

        private Newspaper GetNewspaperEntity(XElement el)
        {
            Newspaper newspaper = new Newspaper();
            newspaper.Name = el.Element(el.Name.Namespace + nameof(newspaper.Name)).Value;
            newspaper.PublishCity = el.Element(el.Name.Namespace + nameof(newspaper.PublishCity)).Value;
            newspaper.PublishingHouse = el.Element(el.Name.Namespace + nameof(newspaper.PublishingHouse)).Value;
            newspaper.PublishYear =
                XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(newspaper.PublishYear)).Value);
            newspaper.PageCount = XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(newspaper.PageCount)).Value);
            newspaper.Comment = el.Element(el.Name.Namespace + nameof(newspaper.Comment)).Value;
            newspaper.SerialNumber =
                XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(newspaper.SerialNumber)).Value);
            newspaper.Date = XmlConvert.ToDateTime(el.Element(el.Name.Namespace + nameof(newspaper.Date)).Value);
            newspaper.ISSN = el.Element(el.Name.Namespace + nameof(newspaper.ISSN)).Value;
            return newspaper;

        }

        private Patent GetPatentEntity(XElement el)
        {
            Patent patent = new Patent();
            patent.Name = el.Element(el.Name.Namespace + nameof(patent.Name)).Value;
            patent.Creator = el.Element(el.Name.Namespace + nameof(patent.Creator)).Value;
            patent.Country = el.Element(el.Name.Namespace + nameof(patent.Country)).Value;
            patent.SerialNumber = XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(patent.SerialNumber)).Value);
            patent.RequestDate =
                XmlConvert.ToDateTime(el.Element(el.Name.Namespace + nameof(patent.RequestDate)).Value);
            patent.PublishDate =
                XmlConvert.ToDateTime(el.Element(el.Name.Namespace + nameof(patent.PublishDate)).Value);
            patent.PageCount = XmlConvert.ToInt32(el.Element(el.Name.Namespace + nameof(patent.PageCount)).Value);
            patent.Comment = el.Element(el.Name.Namespace + nameof(patent.Comment)).Value;
            return patent;
        }

        private IEnumerable<Book> GetBooks()
        {
            return booksRepository.GetData();
        }

        private IEnumerable<Newspaper> GetNewsPapers()
        {
            return newsPapersRepository.GetData();
        }

        private IEnumerable<Patent> GetPatents()
        {
            return patentsRepository.GetData();
        }

        private XElement WriteBookToXml(XNamespace books, Book book)
        {
            return new XElement(books + nameof(book),
                new XElement(books + nameof(book.Name), book.Name),
                new XElement(books + nameof(book.Authors), book.Authors),
                new XElement(books + nameof(book.PublishCity), book.PublishCity),
                new XElement(books + nameof(book.PublishingHouse), book.PublishingHouse),
                new XElement(books + nameof(book.PublishYear), book.PublishYear),
                new XElement(books + nameof(book.PageCount), book.PageCount),
                new XElement(books + nameof(book.Comment), book.Comment),
                new XElement(books + nameof(book.ISBN), book.ISBN));
        }

        private XElement WriteNewsPaperToXml(XNamespace newspapers, Newspaper newspaper)
        {
            return new XElement(newspapers + nameof(newspaper),
                new XElement(newspapers + nameof(newspaper.Name), newspaper.Name),
                new XElement(newspapers + nameof(newspaper.PublishCity), newspaper.PublishCity),
                new XElement(newspapers + nameof(newspaper.PublishingHouse), newspaper.PublishingHouse),
                new XElement(newspapers + nameof(newspaper.PublishYear), newspaper.PublishYear),
                new XElement(newspapers + nameof(newspaper.PageCount), newspaper.PageCount),
                new XElement(newspapers + nameof(newspaper.Comment), newspaper.Comment),
                new XElement(newspapers + nameof(newspaper.SerialNumber), newspaper.SerialNumber),
                new XElement(newspapers + nameof(newspaper.Date), newspaper.Date),
                new XElement(newspapers + nameof(newspaper.ISSN), newspaper.ISSN));
        }

        private XElement WritePatentToXml(XNamespace patents, Patent patent)
        {
            return new XElement(patents + nameof(patent),
                new XElement(patents + nameof(patent.Name), patent.Name),
                new XElement(patents + nameof(patent.Creator), patent.Creator),
                new XElement(patents + nameof(patent.Country), patent.Country),
                new XElement(patents + nameof(patent.SerialNumber), patent.SerialNumber),
                new XElement(patents + nameof(patent.RequestDate), patent.RequestDate),
                new XElement(patents + nameof(patent.PublishDate), patent.PublishDate),
                new XElement(patents + nameof(patent.PageCount), patent.PageCount),
                new XElement(patents + nameof(patent.Comment), patent.Comment));
        }
    }
}
