using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.PharmacyServices.Interfaces
{
    public interface IPrescriptionService
    {
        Task<string> GenerateJson(Precision.API.Model.PharmacyInfo.PrescriptionOrder order, string processedFilePath, string id, Actions action);
    }
}
