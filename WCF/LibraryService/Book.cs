using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Author { get; set; }

        [DataMember]
        public int Year { get; set; }

        [DataMember]
        public BookType BookType { get; set; }

        protected bool Equals(Book other)
        {
            return Id == other.Id && string.Equals(Name, other.Name) && string.Equals(Author, other.Author) &&
                   Year == other.Year && BookType == other.BookType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Book) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode*397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Author != null ? Author.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ Year;
                hashCode = (hashCode*397) ^ (int) BookType;
                return hashCode;
            }
        }
    }
}