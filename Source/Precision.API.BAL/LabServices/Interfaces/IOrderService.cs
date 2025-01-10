using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.LabServices.Interfaces
{
    public interface IOrderService
    {
        Task<string> GenerateCSV(Order order, string processedFilePath, string id, LabResource fhirResource);
    }
}
