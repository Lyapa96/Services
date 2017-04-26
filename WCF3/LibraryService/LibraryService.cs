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
        private const int DaysInMonth = 30;

        private static List<Book> books;
        private static Dictionary<int, DateTime> bookIdToIssueDate;
        private static List<ClientCard> clientCards;

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

            bookIdToIssueDate = bookIdToIssueDate ?? new Dictionary<int, DateTime>
            {
                {1, new DateTime(2000, 11, 11)}
            };

            clientCards = clientCards ?? new List<ClientCard>
            {
                new ClientCard
                {
                    Firstname = "Петр",
                    Lastname = "Петров",
                    BooksIDs = new List<int>
                    {
                        1
                    }
                },
                new ClientCard
                {
                    Firstname = "Иван",
                    Lastname = "Иванов",
                    BooksIDs = new List<int>
                    {
                        3
                    }
                }
            };
        }

        private ILibraryServiceCallback Callback
            => OperationContext.Current.GetCallbackChannel<ILibraryServiceCallback>();

        private string currentClientLastname { get; set; }
        private string currentClientFirstname { get; set; }

        public void Dispose()
        {
            Console.WriteLine($"Сессия(ID = {OperationContext.Current.SessionId}) завершена");
        }

        public void Present(string firstName, string lastName)
        {
            currentClientFirstname = firstName;
            currentClientLastname = lastName;
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
            var book = books.FirstOrDefault(i => i.Id == id);
            if (book != null) return book;
            Console.WriteLine($"Книги (id: {id}) не найдено");
            throw new FaultException($"Книги (id: {id}) не найдено");
        }

        public List<Book> GetBooks(string author)
        {
            Console.WriteLine($"Клиент попросил все книги автора: {author}");
            return books.Where(i => i.Author == author).ToList();
        }

        public void Take(Book book)
        {
            var clientCard = GetCurrentClientCard();
            if (clientCard.BooksIDs.Count < MaximumBookCountForClinet)
            {
                if (books.Contains(book))
                {
                    Console.WriteLine($"Клиент взял книгу: {book.Name}");
                    clientCard.BooksIDs.Add(book.Id);
                    bookIdToIssueDate.Add(book.Id,DateTime.Now);
                    books.Remove(book);
                }
                else
                {
                    Console.WriteLine("Книга занята");
                    throw new FaultException("Книга занята");
                }
            }
            else
            {
                Console.WriteLine("В библиотетке действует правило:«В одни руки не больше 5 книг»");
            }
        }

        public void Return(Book book)
        {
            var clientCard = GetCurrentClientCard();
            clientCard.BooksIDs.Remove(book.Id);
            Console.WriteLine($"Клиент вернул книгу: {book.Name}");
            books.Add(book);
        }

        public int GetBooksCount()
        {
            return books.Count;
        }

        public void ApplyСhanges()
        {
            if (ClientHasBooksOnHandMoreThanMonth())
            {
                Console.WriteLine("Где книги и когда принесёшь?");
                Callback.OnCallback();
            }
            Console.WriteLine("Изменения применены");
        }

        [OperationBehavior(ReleaseInstanceMode = ReleaseInstanceMode.AfterCall)]
        public void EscapeLibrary()
        {
            Console.WriteLine("Клиент покинул здание");
        }

        private bool ClientHasBooksOnHandMoreThanMonth()
        {
            var clientCard = GetCurrentClientCard();
            return clientCard.BooksIDs.Any(bookId => (bookIdToIssueDate[bookId] - DateTime.Now).TotalDays > DaysInMonth);
        }

        private ClientCard GetCurrentClientCard()
        {
            return clientCards.First(x => x.Lastname == currentClientLastname && x.Firstname == currentClientFirstname);
        }
    }
}