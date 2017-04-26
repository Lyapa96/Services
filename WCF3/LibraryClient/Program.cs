using System;
using System.ServiceModel;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var instanceContext1 = new InstanceContext(new ClientCallback());
            var client1 = new LibraryServiceClient(instanceContext1);
            client1.Present("Иван", "Иванов");
            client1.EscapeLibrary();

            var instanceContext2 = new InstanceContext(new ClientCallback());
            var client2 = new LibraryServiceClient(instanceContext2);
            client2.Present("Петр", "Петров");
            Console.WriteLine($"ID-сессии: {client2.InnerChannel.SessionId}");

            var journal = client2.Get(2);
           
            try
            {
                client2.Get(45);
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Reason);
            }

            client2.Take(journal);

            try
            {
                var book = new Book() {Id = 45};
                client2.Take(book);
            }
            catch (FaultException ex)
            {
                Console.WriteLine(ex.Reason);
            }

            client2.ApplyСhanges();

            Console.ReadLine();
        }
    }
}