using APIEarnMoney.Models.Requests;
using RestSharp;
using RestSharp.Serializers.NewtonsoftJson;
using System.Net;

namespace APIEarnMoney.Helpers
{
    public static class RestHelper
    {


        public static async Task<RestResponse> SendText(string message)
        {
            try
            {
                var client = new RestClient(EarnMoneyRouter.WhatsAppURL, configureSerialization: s => s.UseNewtonsoftJson());
                var request = new RestRequest("", Method.Post);
                request.AddBody(new WhatsAppSendTextRequest(EarnMoneyRouter.WhatsAppNumber, message).ToStringx(), contentType: ContentType.Json);
                var result = await client.ExecuteAsync(request);
                return result;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static ICollection<KeyValuePair<string, string>> CreateHeader(string token)
        {
            return
            [
                new KeyValuePair<string, string>("log-header"," I am the log request header."),
                new KeyValuePair<string, string>("token",token),
                new KeyValuePair<string, string>("Content-Type","application/x-www-form-urlencoded"),
                new KeyValuePair<string, string>("Host","admin.tgldy.xyz"),
                new KeyValuePair<string, string>("Connection","Keep-Alive"),
                new KeyValuePair<string, string>("Accept-Encoding","gzip"),
                new KeyValuePair<string, string>("User-Agent","okhttp/4.10.0"),
            ];
        }
        public static async Task<RestResponse<T>> GetResponse<T>(string path, string token, object body = null!) where T : class 
        {
            try
            {
                RestClient client = new(EarnMoneyRouter.BaseUrl, configureSerialization: s => s.UseNewtonsoftJson());
                RestRequest request = new(path, Method.Post);
                if(body != null)
                {
                    request.AddBody(body.ToString()!, ContentType.FormUrlEncoded);
                }
                request.AddHeaders(CreateHeader(token));
                return await client.ExecuteAsync<T>(request);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public static async Task<RestResponse> GetRedirect(string url)
        {
            try
            {
                var client = new RestClient(url, configureSerialization: s => s.UseNewtonsoftJson());
                var request = new RestRequest("", Method.Get);
                var response = await client.ExecuteAsync(request);
                if (response.StatusCode == HttpStatusCode.Found)
                {
                    var location = response.Headers!.FirstOrDefault(e => e.Name == "Location");
                    if (location != null)
                    {
                        await GetRedirect(location!.Value!.ToString()!);
                    }
                }
                return response;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
