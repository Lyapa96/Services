using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService
{
    public class LibraryService : ILibraryService
    {
        private static List<Book> books;

        public LibraryService()
        {
            books = books ?? new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Author = "Пушкин А.С.",
                    BookType = BookType.Tale,
                    Name = "Сказка о попе и о работнике его Балде"
                },
                new Book
                {
                    Id = 2,
                    Author = "Журналисты",
                    BookType = BookType.Journal,
                    Name = "Cosmopolitan",
                    Year = 2017
                },
                new Book
                {
                    Id = 3,
                    Author = "John Doe",
                    BookType = BookType.Encyclopedia,
                    Name = "About everything",
                    Year = 1996
                }
            };
        }

        public void Add(Book book)
        {
            Console.WriteLine("Add");
            books.Add(book);
        }

        public Book Get(int id)
        {
            Console.WriteLine("Get");
            return books.FirstOrDefault(i => i.Id == id);
        }

        public List<Book> GetBooks(string author)
        {
            Console.WriteLine("GetBooks");
            return books.Where(i => i.Author == author).ToList();
        }

        public void Take(Book book)
        {
            Console.WriteLine("Take");
            if (books.Contains(book))
            {
                books.Remove(book);
            }
        }

        public void Return(Book book)
        {
            Console.WriteLine("Return");
            books.Add(book);
        }

        public int GetBooksCount()
        {
            return books.Count;
        }
    }
}