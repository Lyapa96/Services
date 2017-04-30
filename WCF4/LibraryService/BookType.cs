using System.Runtime.Serialization;

namespace LibraryService
{
    [DataContract]
    public enum BookType
    {
        [EnumMember]
        Journal,
        [EnumMember]
        Encyclopedia,
        [EnumMember]
        Tale
    }
}