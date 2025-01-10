using Nancy.Extensions;
using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;
using System.Text.Json;

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
        
        public async Task<HttpResponseMessage> Save(Order order, string processedFilePath, LabCredential credential, LabResource labResource, string id = "")
        {
            await _commonMethods.CreateOrAppendFile(processedFilePath, string.Concat("--- Save ", labResource.ToString(), " Started ---"));

            HttpResponseMessage? response = null;

            string _str = labResource switch
            {
                LabResource.CreateOrder => await _orderService.GenerateCSV(order, processedFilePath, id, labResource),
            };

            credential.Url = string.Concat(credential.Url, "orderAPI.cgi");

            response = await _httpService.PostRequestWithFile(credential, labResource.ToString(), _str, processedFilePath);

            string result = response.Content.ReadAsStringAsync().Result;

// Not able to parse below json
//            {
//                "orders":[{
//                    "ordnum":"DocChart123122144", "client":Native American, "error":"Row 2: Missing Account Number.
//"}],"success": false}

            try
            {
                var jObj = (JObject)JsonConvert.DeserializeObject(result);

                if (!Convert.ToBoolean(jObj["success"]))
                {
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    response.ReasonPhrase = jObj["msg"].ToString().RemoveUselessChars();
                }
            } catch
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = result.RemoveUselessChars();
            }

            return response;
        }
    }
}