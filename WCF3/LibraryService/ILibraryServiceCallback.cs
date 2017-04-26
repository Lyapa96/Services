using System.ServiceModel;

namespace LibraryService
{
    [ServiceContract]
    public interface ILibraryServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void OnCallback();
    }
}