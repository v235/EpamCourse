using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using XML.Task.DAL;
using XML.Task.Entity;

namespace XMLLibrary
{
    public class XmlManager
    {
        private readonly IRepository<Book> booksRepository;
        private readonly IRepository<Newspaper> newsPapersRepository;
        private readonly IRepository<Patent> patentsRepository;

        public XmlManager()
        {
            booksRepository = new BooksRepository();
            newsPapersRepository = new NewspapersRepository();
            patentsRepository = new PatentsRepository();
        }

        public StringBuilder CreateXml(string nsString)
        {
            StringBuilder sb = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(sb,
                new XmlWriterSettings {Indent = true}))
            {
                xmlWriter.WriteStartElement("library", nsString);
                xmlWriter.WriteStartElement("books", nsString + "books");
                foreach (var book in GetBooks())
                {
                    WriteBookToXml(XNamespace.Get(nsString + "books"), book).WriteTo(xmlWriter);
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("newspapers", nsString + "newspapers");
                foreach (var newsPaper in GetNewsPapers())
                {
                    WriteNewsPaperToXml(XNamespace.Get(nsString + "newspapers"), newsPaper).WriteTo(xmlWriter);
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteStartElement("patents", nsString + "patents");
                foreach (var patent in GetPatents())
                {
                    WritePatentToXml(XNamespace.Get(nsString + "patents"), patent).WriteTo(xmlWriter);
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteEndElement();
                xmlWriter.Close();
                return sb;
            }
        }

        public IEnumerable<XElement> GetDataFromXML(StringBuilder xml, string elementName)
        {
            using (XmlReader reader = XmlReader.Create(new StringReader(xml.ToString())))
            {
                reader.MoveToContent();
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == elementName)
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                yield return el;
                            }
                        }
                    }
                }
            }
        }

        //public void SaveDataToXml(StringBuilder sb)
        //{
        //    using (XmlWriter xmlWriter = XmlWriter.Create("test.xml",
        //        new XmlWriterSettings {Indent = true}))
        //    {
        //        sb.AppendFormat()
        //    }
        //}

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
            return new XElement(books+ nameof(book),
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
            return new XElement(newspapers+nameof(newspaper),
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
            return new XElement(patents+nameof(patent),
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
