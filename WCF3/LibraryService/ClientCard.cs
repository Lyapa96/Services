using System;
using System.Collections.Generic;

namespace LibraryService
{
    public class ClientCard
    {
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public List<int> BooksIDs { get; set; }
    }
}