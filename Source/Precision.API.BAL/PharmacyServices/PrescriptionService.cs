using Nancy.Json;
using Newtonsoft.Json;
using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.BAL.PharmacyServices.Interfaces;
using Precision.API.Model.Enums;

namespace Precision.API.BAL.PharmacyServices
{
    public class PrescriptionService : IPrescriptionService
    {
        private readonly ICommonMethods _common;
        public PrescriptionService(ICommonMethods commonMethods)
        {
            _common = commonMethods;
        }
        public async Task<string> GenerateJson(Precision.API.Model.PharmacyInfo.PrescriptionOrder order, string processedFilePath, string id, Actions action)
        {
            string json = JsonConvert.SerializeObject(order);

            return json.Replace("{\"referenceId\"", "{\"requestType\":\"create\",\"referenceId\"");
        }
        public async Task<string> GenerateCancelRequestJson(string processedFilePath, string id)
        => JsonConvert.SerializeObject(new { externalRxNumber = id });
    }
}