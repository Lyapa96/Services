using System.ServiceModel;
using System.ServiceModel.Web;

namespace LibraryService
{
    [ServiceContract]
    public interface ILibraryService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/", ResponseFormat = WebMessageFormat.Json)]
        BookCollection GetAllBooks();

        [OperationContract]
        [WebGet(UriTemplate = "/{author}", ResponseFormat = WebMessageFormat.Json)]
        BookCollection GetBooksByAuthor(string author);

        [OperationContract]
        [WebGet(UriTemplate = "/{author}/{id}", ResponseFormat = WebMessageFormat.Json)]
        Book GetBookById(string author, string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/",Method = "POST",RequestFormat = WebMessageFormat.Json,ResponseFormat = WebMessageFormat.Json)]
        string Add(Book book);
    }
}