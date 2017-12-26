using System;
using System.Xml.Serialization;

namespace SerializationLib
{
    public enum Genre
    {
        [XmlEnum(Name = "Computer")]
        Computer = 1,

        [XmlEnum(Name = "Fantasy")]
        Fantasy = 2,

        [XmlEnum(Name = "Romance")]
        Romance = 3,

        [XmlEnum(Name = "Horror")]
        Horror = 4,

        [XmlEnum(Name = "Science Fiction")]
        Science_Fiction = 5
    }

    [Serializable]
    public class Book
    {
        [XmlAttribute ("id")]
        public string Id { get; set; }

        [XmlElement ("isbn")]
        public string Isbn { get; set; }

        [XmlElement("author")]
        public string Author { get; set; }

        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("genre")]
        public Genre Genre { get; set; }

        [XmlElement("publisher")]
        public string Publisher { get; set; }

        [XmlElement("publish_date")]
        public DateTime PublishDate { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("registration_date")]
        public DateTime RegistrationDate { get; set; }
    }
}
