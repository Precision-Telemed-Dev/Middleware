using Precision.API.Model.Common;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage?> PostRequestWithFile(Credential credential, string _resource, string st, string processedFilePath);
        Task<HttpResponseMessage?> PostRequest(Credential credential, string _resource, string _json, string processedFilePath);
        Task<HttpResponseMessage?> GetRequest(Credential credential, string action, string processedFilePath, string filter, bool isBasicAuth = true);
    }
}
