using Precision.API.Model.Common;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage?> PostRequestWithFile(Credential credential, string _resource, string st, string processedFilePath);
    }
}
