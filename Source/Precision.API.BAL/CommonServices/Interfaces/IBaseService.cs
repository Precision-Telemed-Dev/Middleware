using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IBaseService
    {
        Task<HttpResponseMessage> SavePharmacy(string processedFilePath, Credential credential, Actions action, object order, string id = "");
        Task<HttpResponseMessage> Get(string processedFilePath, Credential credential, string id, Actions action);
        Task<HttpResponseMessage> SaveLab(LabOrder labOrder, string processedFilePath, Credential credential, Actions action,
            string pharClientNumber, string PharPhysicianNumber, string id = "");
        
    }
}
