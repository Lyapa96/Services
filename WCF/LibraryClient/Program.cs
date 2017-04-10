using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var client = new LibraryServiceClient();

            var journal = client.Get(3);
            PrintBookInfo(journal);

            var newBook = new Book
            {
                Id = 4,
                Author = "Пушкин А.С.",
                BookType = BookType.Tale,
                Name = "Сказка о рыбаке и рыбке"
            };
            client.Add(newBook);

            var books = client.GetBooks("Пушкин А.С.");
            foreach (var book in books)
            {
                PrintBookInfo(book);
            }

            Console.WriteLine($"Количество книг в библиотеке: {client.GetBooksCount()}");

            Console.WriteLine("Клиент забрал журнал");
            client.Take(journal);
            Console.WriteLine($"Количество книг в библиотеке: {client.GetBooksCount()}");

            Console.WriteLine("Клиент вернул журнал");
            client.Return(journal);
            Console.WriteLine($"Количество книг в библиотеке: {client.GetBooksCount()}");

            Console.ReadLine();
        }

        private static void PrintBookInfo(Book book)
        {
            Console.WriteLine($"ID = {book.Id}");
            Console.WriteLine($"Название: {book.Name}");
            Console.WriteLine($"Автор: {book.Author}");
        }
    }
}
