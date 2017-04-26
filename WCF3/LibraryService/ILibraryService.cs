using System.Collections.Generic;
using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract(SessionMode = SessionMode.Required,CallbackContract = typeof(ILibraryServiceCallback))]
    public interface ILibraryService
    {
        [OperationContract(IsInitiating = true)]
        void Present(string firstName, string lastName);

        [OperationContract(IsInitiating = false)]
        void Add(Book book);

        [OperationContract(IsInitiating = false)]
        Book Get(int id);

        [OperationContract(IsInitiating = false)]
        List<Book> GetBooks(string author);

        [OperationContract(IsInitiating = false)]
        void Take(Book book);

        [OperationContract(IsInitiating = false)]
        void Return(Book book);

        [OperationContract(IsInitiating = false)]
        int GetBooksCount();

        [OperationContract(IsInitiating = false, IsTerminating = true)]
        void ApplyСhanges();

        [OperationContract(IsInitiating = false,IsOneWay = true)]
        void EscapeLibrary();
    }
}