using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IBaseService
    {
        Task<HttpResponseMessage> Save(Order order, string processedFilePath, LabCredential credential, string id = "");
    }
}
