using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
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
            HttpRequestMessage message = new()
            {
                Method = requestDto.Method,
                RequestUri = new Uri(requestDto.Url)
            };
            message.Headers.Add("Accept", "application/json");
            if(requestDto.Body != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Body), 
                    Encoding.UTF8, "application/json");
            }

            // 2. construct httpClient and send request.
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpResponseMessage apiResponse = await client.SendAsync(message);

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        }
    }
}
