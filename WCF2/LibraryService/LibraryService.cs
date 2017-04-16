using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    public class LibraryService : ILibraryService, IDisposable
    {
        private const int MaximumBookCountForClinet = 5;

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
                    Author = "Какой то главный редактор",
                    BookType = BookType.Journal,
                    Name = "Теленеделя",
                    Year = 2017
                },
                new Book
                {
                    Id = 5,
                    Author = "Стив Джобс",
                    BookType = BookType.Encyclopedia,
                    Name = "Мир apple",
                    Year = 2003
                }
            };
        }

        private int CurrentBookCount { get; set; }

        public void Dispose()
        {
            Console.WriteLine($"Сессия(ID = {OperationContext.Current.SessionId}) завершена");
        }

        public void Present(string firstName, string lastName)
        {
            Console.WriteLine(
                $"Добро пожаловать в библиотеку,{lastName} {firstName}!\n\rID-сессии: {OperationContext.Current.SessionId}");
        }

        public void Add(Book book)
        {
            Console.WriteLine($"Клиент принес в библиотеку новую книгу: {book.Name}");
            books.Add(book);
        }

        public Book Get(int id)
        {
            Console.WriteLine("Get");
            return books.FirstOrDefault(i => i.Id == id);
        }

        public List<Book> GetBooks(string author)
        {
            Console.WriteLine($"Клиент попросил все книги автора: {author}");
            return books.Where(i => i.Author == author).ToList();
        }

        public void Take(Book book)
        {
            if (CurrentBookCount < MaximumBookCountForClinet)
            {
                CurrentBookCount++;
                Console.WriteLine($"Клиент взял книгу: {book.Name}");
                if (books.Contains(book))
                {
                    books.Remove(book);
                }
            }
            else
            {
                Console.WriteLine("В библиотетке действует правило:«В одни руки не больше 5 книг»");
            }
        }

        public void Return(Book book)
        {
            CurrentBookCount--;
            Console.WriteLine($"Клиент вернул книгу: {book.Name}");
            books.Add(book);
        }

        public int GetBooksCount()
        {
            return books.Count;
        }

        public void ApplyСhanges()
        {
            Console.WriteLine("Изменения применены");
        }

        [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.AfterCall)]
        public void EscapeLibrary()
        {
            Console.WriteLine("Клиент покинул здание");
        }
    }
}