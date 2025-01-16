using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.LabServices.Interfaces
{
    public interface ILabOrderService
    {
        Task<string> GenerateCSV(LabOrder order);
    }
}
