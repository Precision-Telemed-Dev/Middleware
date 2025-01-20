using Newtonsoft.Json;
using Precision.API.Model.Common;

namespace Precision.Authorization
{
    public static class AuthorizeSession
    {
        public static string accessToken = string.Empty;    
        public async static Task<HttpResponseMessage> Authorize(Credential credential)
        {
            Uri uri = new Uri(string.Concat(credential.Url, "authAPI.cgi", "?mode=" + credential.Mode + "&username=" + credential.Username + "&password=" + credential.Password));

            HttpClient client = new();
            HttpRequestMessage request = new(HttpMethod.Get, uri.ToString());

            HttpResponseMessage httpResponseMessage = await client.SendAsync(request);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                string responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                dynamic dynamicResponse = JsonConvert.DeserializeObject(responseContent);
                credential.SessionKey = accessToken = dynamicResponse.sessionkey;
            }
            else
                accessToken = string.Empty;

            return httpResponseMessage;
        }
    }
}
