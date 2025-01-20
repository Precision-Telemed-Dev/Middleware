using Precision.API.BAL.CommonServices.Interfaces;
using Precision.API.Model.Common;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace Precision.API.BAL.CommonServices
{
    public class HttpService : IHttpService
    {
        private readonly ICommonMethods _common;

        public HttpService(ICommonMethods commonMethods) { _common = commonMethods; }
        public async Task<HttpResponseMessage?> PostRequestWithFile(Credential credential, string _resource, string st, string processedFilePath)
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
        public async Task<HttpResponseMessage?> PostRequest(Credential credential, string _resource, string _json, string processedFilePath)
        {
            await _common.CreateOrAppendFile(processedFilePath, string.Concat("- Post ", _resource));
            await _common.CreateOrAppendFile(processedFilePath, string.Concat("RequestJSON -> ", _json));

            HttpContent _content = new StringContent(_json, Encoding.UTF8, "application/json");

            var client = new HttpClient();

            var authenticationString = $"{credential.Username}:{credential.Password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync(credential.Url,_content);

            return response;
        }
        public async Task<HttpResponseMessage?> GetRequest(Credential credential, string action, string processedFilePath, string _id)
        {
            await _common.CreateOrAppendFile(processedFilePath, string.Concat("- Get ", action.ToString(), " (", _id, ")"));

            var client = new HttpClient();

            var authenticationString = $"{credential.Username}:{credential.Password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authenticationString));

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
            client.DefaultRequestHeaders
              .Accept
              .Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(string.Concat(credential.Url, _id));

            return response;
        }
    }    
}