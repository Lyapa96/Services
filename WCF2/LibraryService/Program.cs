using System;
using System.ServiceModel;

namespace LibraryService
{
    public class Program
    {
        public static void Main()
        {
            var host = new ServiceHost(typeof(LibraryService));

            host.Open();

            Console.WriteLine("Press enter...");
            Console.ReadLine();

            host.Close();
        }
    }
}