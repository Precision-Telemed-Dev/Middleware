using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.CommonServices.Interfaces
{
    public interface IBaseService
    {
        Task<HttpResponseMessage> Save(LabOrder labOrder, string processedFilePath, Credential credential, Actions action, string id = "");
        Task<HttpResponseMessage> SavePharmacy(Precision.API.Model.PharmacyInfo.PrescriptionOrder order, string processedFilePath, Credential credential, Actions action, string id = "");
        Task<HttpResponseMessage> Get(string processedFilePath, Credential credential, string id, Actions action);
    }
}
