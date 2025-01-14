using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.PharmacyServices.Interfaces
{
    public interface IPrescriptionService
    {
        Task<string> GenerateJson(Precision.API.Model.PharmacyInfo.PrescriptionOrder order);
        Task<string> GenerateCancelRequestJson(string processedFilePath, string id);
        Task<string> GenerateRefillJson(Precision.API.Model.PharmacyInfo.RefillOrder order);
    }
}
