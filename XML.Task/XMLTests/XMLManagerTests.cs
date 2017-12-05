using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Data.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using XML.Task.DAL;
using XML.Task.Entity;
using XMLLibrary;

namespace XMLTests
{
    [TestClass]
    public class XMLManagerTests
    {
        IRepository<Book> mockBookRepository;
        IRepository<Newspaper> mockNewspaperRepository;
        IRepository<Patent> mockPatentRepository;
        XmlManager manager;

        [TestInitialize]
        public void Initialize()
        {
            mockBookRepository = MockRepository.GenerateStub<IRepository<Book>>();
            mockBookRepository.Stub(s => s.GetData()).Return(new[]
            {
                new Book()
                {
                    Name = "TestBook",
                    Authors = "TestAuthorBook",
                    PublishCity = "TestCityBook",
                    PublishingHouse = "TestPublishingHouseBook",
                    PublishYear = 2000,
                    PageCount = 0,
                    Comment = "TestCommentBook",
                    ISBN = "00000"
                }
            });
            mockNewspaperRepository = MockRepository.GenerateStub<IRepository<Newspaper>>();
            mockNewspaperRepository.Stub(s => s.GetData()).Return(new[]
            {
                new Newspaper()
                {
                    Name = "TestNameNewspaper",
                    PublishCity = "TestCityNewpaper",
                    PublishingHouse = "TestPublishingHouseNewspaper",
                    PublishYear = 2000,
                    PageCount = 20,
                    Comment = "TestCommentNewspaper",
                    SerialNumber = 1,
                    Date = DateTime.Parse("2008-05-01 7:34:42Z"),
                    ISSN = "0734-7456"
                }
            });
            mockPatentRepository = MockRepository.GenerateStub<IRepository<Patent>>();
            mockPatentRepository.Stub(s => s.GetData()).Return(new[]
            {
                new Patent()
                {
                    Name = "TestNamePatent",
                    Creator = "TestCreaterPatent",
                    Country = "TestCountryPatent",
                    SerialNumber = 1,
                    RequestDate = DateTime.Parse("2005-02-01 7:34:42Z"),
                    PublishDate = DateTime.Parse("2014-08-05 7:34:42Z"),
                    PageCount = 20,
                    Comment = "TestCommentPatent"
                }
            });
            manager = new XmlManager(mockBookRepository, mockNewspaperRepository, mockPatentRepository);
        }

        [TestMethod]
        public void WriteXmlFileTestWithRightResult()
        {
            // Arrange
            string date = DateTime.Today.ToString(); 
            string expect =
                "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<catalog dounloadtime=\"" +
                date+
                "\" BooksCount=\"1\" NewspapersCount=\"1\" PatentCount=\"1\" xmlns=\"test\">\r\n  " +
                "<book>\r\n    <Name>TestBook</Name>\r\n    <Authors>TestAuthorBook</Authors>\r\n    " +
                "<PublishCity>TestCityBook</PublishCity>\r\n    <PublishingHouse>TestPublishingHouseBook</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>0</PageCount>\r\n    <Comment>TestCommentBook</Comment>\r\n    " +
                "<ISBN>00000</ISBN>\r\n  </book>\r\n  <newspaper>\r\n    <Name>TestNameNewspaper</Name>\r\n    " +
                "<PublishCity>TestCityNewpaper</PublishCity>\r\n    <PublishingHouse>TestPublishingHouseNewspaper</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>20</PageCount>\r\n    <Comment>TestCommentNewspaper</Comment>\r\n    " +
                "<SerialNumber>1</SerialNumber>\r\n    <Date>2008-05-01T10:34:42+03:00</Date>\r\n    <ISSN>0734-7456</ISSN>\r\n  " +
                "</newspaper>\r\n  <patent>\r\n    <Name>TestNamePatent</Name>\r\n    <Creator>TestCreaterPatent</Creator>\r\n    " +
                "<Country>TestCountryPatent</Country>\r\n    <SerialNumber>1</SerialNumber>\r\n    <RequestDate>2005-02-01T09:34:42+02:00</RequestDate>\r\n    " +
                "<PublishDate>2014-08-05T10:34:42+03:00</PublishDate>\r\n    <PageCount>20</PageCount>\r\n    " +
                "<Comment>TestCommentPatent</Comment>\r\n  </patent>\r\n</catalog>";
            MemoryStream sb = new MemoryStream();
            // Act
            manager.WriteXmlFile(sb, "test");
            var result = Encoding.UTF8.GetString(sb.ToArray());
            // Assert
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void WriteXmlFileTestWithNotRightResult()
        {
            // Arrange
            string expect = "";
            MemoryStream sb = new MemoryStream();
            // Act
            manager.WriteXmlFile(sb, "test");
            var result = Encoding.UTF8.GetString(sb.ToArray());
            // Assert
            Assert.AreNotEqual(expect, result);
        }
        [TestMethod]
        public void ReadXmlFileTestWithRightResult()
        {
            // Arrange
            var expected = new List<BaseEntity>();
            expected.Add(new Book()
            {
                Name = "TestBook",
                Authors = "TestAuthorBook",
                PublishCity = "TestCityBook",
                PublishingHouse = "TestPublishingHouseBook",
                PublishYear = 2000,
                PageCount = 0,
                Comment = "TestCommentBook",
                ISBN = "00000"
            });
            expected.Add(new Newspaper()
            {
                Name = "TestNameNewspaper",
                PublishCity = "TestCityNewpaper",
                PublishingHouse = "TestPublishingHouseNewspaper",
                PublishYear = 2000,
                PageCount = 20,
                Comment = "TestCommentNewspaper",
                SerialNumber = 1,
                Date = DateTime.Parse("2008-05-01 7:34:42Z"),
                ISSN = "0734-7456"
            });
            expected.Add(new Patent()
            {
                Name = "TestNamePatent",
                Creator = "TestCreaterPatent",
                Country = "TestCountryPatent",
                SerialNumber = 1,
                RequestDate = DateTime.Parse("2005-02-01 7:34:42Z"),
                PublishDate = DateTime.Parse("2014-08-05 7:34:42Z"),
                PageCount = 20,
                Comment = "TestCommentPatent"
            });
            string inputXml =
                "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<catalog xmlns=\"test\">\r\n  <book>\r\n    <Name>TestBook</Name>\r\n    " +
                "<Authors>TestAuthorBook</Authors>\r\n    " +
                "<PublishCity>TestCityBook</PublishCity>\r\n    " +
                "<PublishingHouse>TestPublishingHouseBook</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>0</PageCount>\r\n    " +
                "<Comment>TestCommentBook</Comment>\r\n    " +
                "<ISBN>00000</ISBN>\r\n  </book>\r\n  " +
                "<newspaper>\r\n    " +
                "<Name>TestNameNewspaper</Name>\r\n    " +
                "<PublishCity>TestCityNewpaper</PublishCity>\r\n    " +
                "<PublishingHouse>TestPublishingHouseNewspaper</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>20</PageCount>\r\n    " +
                "<Comment>TestCommentNewspaper</Comment>\r\n    " +
                "<SerialNumber>1</SerialNumber>\r\n    " +
                "<Date>2008-05-01T10:34:42+03:00</Date>\r\n    " +
                "<ISSN>0734-7456</ISSN>\r\n  </newspaper>\r\n  <patent>\r\n    " +
                "<Name>TestNamePatent</Name>\r\n    " +
                "<Creator>TestCreaterPatent</Creator>\r\n    " +
                "<Country>TestCountryPatent</Country>\r\n    " +
                "<SerialNumber>1</SerialNumber>\r\n    " +
                "<RequestDate>2005-02-01T09:34:42+02:00</RequestDate>\r\n    " +
                "<PublishDate>2014-08-05T10:34:42+03:00</PublishDate>\r\n    " +
                "<PageCount>20</PageCount>\r\n    " +
                "<Comment>TestCommentPatent</Comment>\r\n  " +
                "</patent>\r\n</catalog>";
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(inputXml);
            writer.Flush();
            stream.Position = 0;
            // Act
            var result = manager.ReadXMLFile(stream);
            // Assert
            Assert.AreEqual(true, CollectionAreEqual(expected, result.ToList()));
        }

        [TestMethod]
        public void ReadXmlFileTestWithNotRightResult()
        {
            // Arrange
            var expected = new List<BaseEntity>();
            expected.Add(new Book()
            {
                Name = "TestBook",
                Authors = "TestAuthorBook",
                PublishCity = "TestCityBook",
                PublishingHouse = "TestPublishingHouseBook",
                PublishYear = 2000,
                PageCount = 0,
                Comment = "TestCommentBook",
                ISBN = "00000"
            });
            expected.Add(new Newspaper()
            {
                Name = "TestNameNewspaper1",
                PublishCity = "TestCityNewpaper",
                PublishingHouse = "TestPublishingHouseNewspaper",
                PublishYear = 2000,
                PageCount = 20,
                Comment = "TestCommentNewspaper",
                SerialNumber = 1,
                Date = DateTime.Parse("2008-05-01 7:34:42Z"),
                ISSN = "0734-7456"
            });
            expected.Add(new Patent()
            {
                Name = "TestNamePatent",
                Creator = "TestCreaterPatent",
                Country = "TestCountryPatent",
                SerialNumber = 1,
                RequestDate = DateTime.Parse("2005-02-01 7:34:42Z"),
                PublishDate = DateTime.Parse("2014-08-05 7:34:42Z"),
                PageCount = 20,
                Comment = "TestCommentPatent"
            });
            string inputXml =
                "﻿<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<catalog xmlns=\"test\">\r\n  <book>\r\n    <Name>TestBook</Name>\r\n    " +
                "<Authors>TestAuthorBook</Authors>\r\n    " +
                "<PublishCity>TestCityBook</PublishCity>\r\n    " +
                "<PublishingHouse>TestPublishingHouseBook</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>0</PageCount>\r\n    " +
                "<Comment>TestCommentBook</Comment>\r\n    " +
                "<ISBN>00000</ISBN>\r\n  </book>\r\n  " +
                "<newspaper>\r\n    " +
                "<Name>TestNameNewspaper</Name>\r\n    " +
                "<PublishCity>TestCityNewpaper</PublishCity>\r\n    " +
                "<PublishingHouse>TestPublishingHouseNewspaper</PublishingHouse>\r\n    " +
                "<PublishYear>2000</PublishYear>\r\n    <PageCount>20</PageCount>\r\n    " +
                "<Comment>TestCommentNewspaper</Comment>\r\n    " +
                "<SerialNumber>1</SerialNumber>\r\n    " +
                "<Date>2008-05-01T10:34:42+03:00</Date>\r\n    " +
                "<ISSN>0734-7456</ISSN>\r\n  </newspaper>\r\n  <patent>\r\n    " +
                "<Name>TestNamePatent</Name>\r\n    " +
                "<Creator>TestCreaterPatent</Creator>\r\n    " +
                "<Country>TestCountryPatent</Country>\r\n    " +
                "<SerialNumber>1</SerialNumber>\r\n    " +
                "<RequestDate>2005-02-01T09:34:42+02:00</RequestDate>\r\n    " +
                "<PublishDate>2014-08-05T10:34:42+03:00</PublishDate>\r\n    " +
                "<PageCount>20</PageCount>\r\n    " +
                "<Comment>TestCommentPatent</Comment>\r\n  " +
                "</patent>\r\n</catalog>";
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(inputXml);
            writer.Flush();
            stream.Position = 0;
            // Act
            var result = manager.ReadXMLFile(stream);
            // Assert
            Assert.AreNotEqual(true, CollectionAreEqual(expected, result.ToList()));
        }

        private bool CollectionAreEqual(List<BaseEntity> expected, List<BaseEntity> actual)
        {
            for (int i = 0; i < expected.Count(); i++)
            {
                switch (expected[i].GetType().Name)
                {
                    case "Book":
                        if (!CompareCollection((Book) expected[i], (Book) actual[i]))
                        {
                            return false;
                        }
                        break;
                    case "Newspaper":
                        if (!CompareCollection((Newspaper) expected[i], (Newspaper) actual[i]))
                        {
                            return false;
                        }
                        break;
                    case "Patent":
                        if (!CompareCollection((Patent) expected[i], (Patent) actual[i]))
                        {
                            return false;
                        }
                        break;
                    default:
                        throw new UnknownType("This type is unknown");
                        break;
                }
            }
            return true;
        }

        private bool CompareCollection(Book expected, Book actual)
        {
            if (expected.Name.Equals(actual.Name) && expected.Authors.Equals(actual.Authors)
                && expected.ISBN.Equals(actual.ISBN) && expected.PublishCity.Equals(actual.PublishCity)
                && expected.PublishingHouse.Equals(actual.PublishingHouse) &&
                expected.PageCount.Equals(actual.PageCount) && (expected.PublishYear != actual.PublishYear) && !expected.Comment.Equals(actual.Comment))
                return true;
            return false;
        }

        private bool CompareCollection(Newspaper expected, Newspaper actual)
        {
            if (expected.Name.Equals(actual.Name) && (expected.Date != actual.Date) &&
                !expected.ISSN.Equals(actual.ISSN) && expected.PublishCity.Equals(actual.PublishCity) &&
                expected.PublishingHouse.Equals(actual.PublishingHouse) && (expected.PageCount != actual.PageCount)
                && (expected.PublishYear != actual.PublishYear) && expected.Comment.Equals(actual.Comment) && expected.SerialNumber.Equals(actual.SerialNumber))
                return true;
            return false;
        }

        private bool CompareCollection(Patent expected, Patent actual)
        {
            if (expected.Name.Equals(actual.Name) && expected.Country.Equals(actual.Country) &&
            expected.Creator.Equals(actual.Creator) && expected.PageCount.Equals(actual.PageCount)
            && expected.PublishDate.Equals(actual.PublishDate) && expected.RequestDate.Equals(actual.RequestDate)
            && expected.SerialNumber.Equals(actual.SerialNumber) && expected.Comment.Equals(actual.Comment))
                return true;
            return false;
        }
    }
}
