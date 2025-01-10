using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;
using System.Text;

namespace Precision.API.BAL.LabServices
{
    public class OrderService : IOrderService
    {
        private readonly ICommonMethods _common;
        public OrderService(ICommonMethods commonMethods)
        {
            _common = commonMethods;
        }
    }
}