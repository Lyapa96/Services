using System;
using LibraryClient.LibraryService;

namespace LibraryClient
{
    public class ClientCallback : ILibraryServiceCallback
    {
        public void OnCallback()
        {
            Console.WriteLine("Узбагойся все будет хорошо");
        }
    }
}