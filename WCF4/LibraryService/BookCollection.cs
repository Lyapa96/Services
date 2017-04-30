using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public class BookCollection
    {
        [DataMember]
        public Book[] Books { get; set; }
    }
}