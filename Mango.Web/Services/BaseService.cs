using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace Mango.Web.Services
{
    public class BaseService(IHttpClientFactory httpClientFactory) : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

        /// <inherit />
        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            // 1. construct HttpRequestMessage
            HttpRequestMessage message = new();
            message.Method = requestDto.Method;
            message.RequestUri = new Uri(requestDto.Url);
            message.Headers.Add("Accept", "application/json");
            if(requestDto.Body != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Body), 
                    Encoding.UTF8, "application/json");
            }

            // 2. construct and send httpClient
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpResponseMessage apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(apiContent);

            //return ConstructReponseDtoFromHttpResponseMessage(response);
        }

        //private static ResponseDto ConstructReponseDtoFromHttpResponseMessage(HttpResponseMessage responseMessage)
        //{
        //    return new ResponseDto()
        //    {
        //        StatusCode = responseMessage.StatusCode,
        //        ErrorMessage = responseMessage.IsSuccessStatusCode ?
        //                        null : GetErrorMessage(responseMessage.StatusCode),
        //        Body = responseMessage.IsSuccessStatusCode ? 
        //                        GetResponseBody(responseMessage.Content) : null
        //    };
        //}

        //private static string GetErrorMessage(HttpStatusCode statusCode)
        //{
        //    switch(statusCode)
        //    {
        //        case HttpStatusCode.Unauthorized:
        //            return "Not Authorized";
        //        case HttpStatusCode.Forbidden:
        //            return "Not allowed";
        //        case HttpStatusCode.NotFound:
        //            return "Not found";
        //        default:
        //            return "Something went wrong.";

        //    }
        //}
        //private static async Task<object?> GetResponseBody(HttpContent content)
        //{
        //    try
        //    {
        //        string contentString = await content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject(contentString);
        //    }
        //    catch(Exception ex)
        //    {
        //        // TODO: Logger should be configured to log the exception details.
        //        return null;
        //    }
        //}
    }
}
