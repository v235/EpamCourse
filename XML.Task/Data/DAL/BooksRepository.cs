using System;
using System.Collections.Generic;
using XML.Task.Entity;

namespace XML.Task.DAL
{
    public class BooksRepository : IRepository<Book>
    {
        public IEnumerable<Book> GetData()
        {
            return new[]
            {
                new Book()
                {
                    Name = "Game of Thrones",
                    Authors = "George R.R. Martin ",
                    PublishCity = "London",
                    PublishingHouse = "Bantam",
                    PublishYear = 1996,
                    PageCount = 1780,
                    Comment = "Mass Market Paperback",
                    ISBN = "0553588486"
                },
                new Book()
                {
                    Name = "The Chronicles of Narnia",
                    Authors = "Pauline Baynes",
                    PublishCity = "Liverpul",
                    PublishingHouse = "HarperCollins",
                    PublishYear = 1956,
                    PageCount = 1480,
                    Comment = "The Chronicles of Narnia (Chronological Order) #1–7",
                    ISBN = "0066238501"
                }
            };
        }
    }
}