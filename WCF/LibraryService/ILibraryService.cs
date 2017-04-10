using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        void Add(Book book);

        [OperationContract]
        Book Get(int id);

        [OperationContract]
        List<Book> GetBooks(string author);

        [OperationContract]
        void Take(Book book);

        [OperationContract]
        void Return(Book book);

        [OperationContract]
        int GetBooksCount();
    }
}