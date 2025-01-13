using Precision.API.Model.Common;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage?> PostRequestWithFile(LabCredential credential, string _resource, string st, string processedFilePath);
        Task<HttpResponseMessage?> PostRequest(LabCredential credential, string _resource, string _json, string processedFilePath);
        Task<HttpResponseMessage?> GetRequest(LabCredential credential, string _resource, string processedFilePath, string _id);
    }
}
