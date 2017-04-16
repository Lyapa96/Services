using System;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client1 = new LibraryServiceClient();
            client1.Present("Петр", "Петров");
            Console.WriteLine($"ID-сессии: {client1.InnerChannel.SessionId}");
            client1.EscapeLibrary();

            var client2 = new LibraryServiceClient();
            client2.Present("Иван", "Иванов");
            Console.WriteLine($"ID-сессии: {client1.InnerChannel.SessionId}");

            var newBook = new Book
            {
                Id = 6,
                Author = "Пушкин А.С.",
                BookType = BookType.Tale,
                Name = "Сказка о рыбаке и рыбке"
            };
            client2.Add(newBook);

            for (var i = 1; i < 7; i++)
            {
                var book = client2.Get(i);
                client1.Take(book);
            }
            client2.ApplyСhanges();

            Console.ReadLine();
        }
    }
}