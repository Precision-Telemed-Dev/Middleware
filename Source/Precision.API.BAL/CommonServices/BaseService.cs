using Newtonsoft.Json;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;
using System.Net;

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

        public async Task<HttpResponseMessage> Get(string processedFilePath, LabCredential credential, string id, Actions action)
        {
            await _commonMethods.CreateOrAppendFile(processedFilePath, string.Concat("--- Get ", action.ToString(), " Started ---"));

            HttpResponseMessage? response = await _httpService.GetRequest(credential, action.ToString(), processedFilePath, id);

            return response;
        }
    }
}