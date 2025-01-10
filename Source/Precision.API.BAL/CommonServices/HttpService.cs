using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.Model.Common;

namespace Precision.API.BAL.CommonServices
{
    public class HttpService : IHttpService
    {
        private readonly ICommonMethods _common;

        public HttpService(ICommonMethods commonMethods) { _common = commonMethods; }
        public async Task<HttpResponseMessage?> PostRequestWithFile(LabCredential credential, string _resource, string st, string processedFilePath)
        {
            await _common.CreateOrAppendFile(processedFilePath, string.Concat("- Post ", _resource));
            await _common.CreateOrAppendFile(processedFilePath, string.Concat("RequestCSV -> ", st));

            using var request = new HttpRequestMessage(HttpMethod.Post, credential.Url);
           
            var values = new Dictionary<string, string>
                {
                    { "mode", "processCSV" },
                    { "sessionkey", credential.SessionKey },
                    { "remoteOrdersFile", st },
                };

            var content = new FormUrlEncodedContent(values);
            
            var client = new HttpClient();

            var response = await client.PostAsync(credential.Url, content);

            return response;
        }
    }
}