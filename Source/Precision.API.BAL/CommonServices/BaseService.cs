using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.LabServices.Interfaces;
using Precision.API.BAL.PharmacyServices.Interfaces;
using Precision.API.Model.Common;
using Precision.API.Model.Enums;
using Precision.API.Model.LabInfo;

namespace Precision.API.BAL.CommonServices
{
    public class BaseService : IBaseService
    {
        private readonly IHttpService _httpService;
        private readonly ICommonMethods _commonMethods;
        private readonly ILabOrderService _labOrderService;
        private readonly IPrescriptionService _prescriptionService;

        public BaseService(IHttpService httpService, ICommonMethods commonMethods, ILabOrderService orderService, IPrescriptionService prescriptionService)
        {
            _httpService = httpService;
            _commonMethods = commonMethods;
            _labOrderService = orderService;
            _prescriptionService = prescriptionService;
        }

        public async Task<HttpResponseMessage> SaveLab(LabOrder labOrder, string processedFilePath, Credential credential, Actions action,
            string pharClientNumber, string PharPhysicianNumber, string id = "")
        {
            await _commonMethods.CreateOrAppendFile(processedFilePath, string.Concat("--- Save ", action.ToString(), " Started ---"));

            HttpResponseMessage? response = null;

            string _str = action switch
            {
                Actions.LabCreateOrder => await _labOrderService.GenerateCSV(labOrder, pharClientNumber, PharPhysicianNumber),
            };

            credential.Url = string.Concat(credential.Url, "orderAPI.cgi");

            response = await _httpService.PostRequestWithFile(credential, action.ToString(), _str, processedFilePath);

            string result = response.Content.ReadAsStringAsync().Result;

            // Not able to parse below json
            //            {
            //                "orders":[{
            //                    "ordnum":"DocChart123122144", "client":Native American, "error":"Row 2: Missing Account Number.
            //"}],"success": false}

            try
            {
                var jObj = (JObject)JsonConvert.DeserializeObject(result);

                if (!Convert.ToBoolean(jObj["success"].ToString()))
                {
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    response.ReasonPhrase = jObj["msg"].ToString().RemoveUselessChars();
                }
            }
            catch
            {
                response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                response.ReasonPhrase = result.RemoveUselessChars();
            }

            return response;
        }
        public async Task<HttpResponseMessage> SavePharmacy(Precision.API.Model.PharmacyInfo.PrescriptionOrder order, string processedFilePath, Credential credential, Actions action, string id = "")
        {
            await _commonMethods.CreateOrAppendFile(processedFilePath, string.Concat("--- Save ", action.ToString(), " Started ---"));

            HttpResponseMessage? response = null;

            string _str = action switch
            {
                Actions.PharmacyCreateRequest => await _prescriptionService.GenerateJson(order, processedFilePath, id, action),
            };

            response = await _httpService.PostRequest(credential, action.ToString(), _str, processedFilePath);

            string result = response.Content.ReadAsStringAsync().Result;

            return response;
        }
        public async Task<HttpResponseMessage> Get(string processedFilePath, Credential credential, string filter, Actions action)
        {
            await _commonMethods.CreateOrAppendFile(processedFilePath, string.Concat("--- Get ", action.ToString(), " Started ---"));

            credential.Url = action switch
            {
                Actions.LabReadResult => string.Concat(credential.Url, "resultAPI.cgi?mode=fetchInbox&sessionkey=", credential.SessionKey, "&", filter)
            };

            HttpResponseMessage? response = await _httpService.GetRequest(credential, action.ToString(), processedFilePath, filter);

            if (action == Actions.LabReadResult)
            {
                string result = response.Content.ReadAsStringAsync().Result;

                try
                {
                    var jObj = (JObject)JsonConvert.DeserializeObject(result);
                    
                    if (!Convert.ToBoolean(jObj["success"].ToString()))
                    {
                        response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                        response.ReasonPhrase = jObj["msg"].ToString().RemoveUselessChars();
                    }
                }
                catch
                {
                    response.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                    response.ReasonPhrase = result.RemoveUselessChars();
                }
            }

            return response;
        }
    }
}