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
                },
                new Book
                {
                    Id = 4,
                    Author = "Журналисты",
                    BookType = BookType.Journal,
                    Name = "Теленеделя",
                    Year = 2017
                },
                new Book
                {
                    Id = 5,
                    Author = "John Doe",
                    BookType = BookType.Encyclopedia,
                    Name = "Мир apple",
                    Year = 2003
                },
                new Book
                {
                    Id = 6,
                    Author = "Пушкин А.С.",
                    BookType = BookType.Tale,
                    Name = "Сказка о царе Салтане"
                }
            };
        }

        public BookCollection GetAllBooks()
        {
            return new BookCollection
            {
                Books = books.ToArray()
            };
        }

        public Book GetBookById(string author, string id)
        {
            var bookId = int.Parse(id);
            return books.Find(x => x.Id == bookId);
        }

        public string Add(Book book)
        {
            books.Add(book);
            return $"Книга {book.Name} успешно добавлена";
        }

        public BookCollection GetBooksByAuthor(string author)
        {
            Console.WriteLine(author);
            return new BookCollection
            {
                Books = books.Where(x => x.Author == author).ToArray()
            };
        }
    }
}