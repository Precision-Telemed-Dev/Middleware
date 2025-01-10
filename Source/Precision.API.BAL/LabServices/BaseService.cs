using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.CommonServices
{
    public class BaseService : IBaseService
    {
        private readonly IHttpService _httpService;
        private readonly ICommonMethods _commonMethods;
        private readonly IOrderService _orderService;

        public BaseService(IHttpService httpService, ICommonMethods commonMethods, IOrderService orderService)
        {
            _httpService = httpService;
            _commonMethods = commonMethods;
            _orderService = orderService;
        }
        
        public async Task<HttpResponseMessage> Save(Order order, string processedFilePath, LabCredential credential, string id = "")
        {
            return new HttpResponseMessage();
        }
    }
}