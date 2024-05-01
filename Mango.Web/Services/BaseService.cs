using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace Mango.Web.Services
{
    public class BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider) : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ITokenProvider _tokenProvider = tokenProvider;

        /// <inherit />
        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool useBearerToken = true)
        {
            // 1. construct HttpRequestMessage
            HttpRequestMessage requestMessage = new()
            {
                Method = requestDto.Method,
                RequestUri = new Uri(requestDto.Url)
            };
            requestMessage.Headers.Add("Accept", "application/json");
            if (useBearerToken)
            {
                var token = _tokenProvider.GetToken();
                requestMessage.Headers.Add("Authorization", $"bearer {token}");
            }

            if(requestDto.Body != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Body), 
                    Encoding.UTF8, "application/json");
            }

            // 2. construct httpClient and send request.
            HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
            HttpResponseMessage apiResponse = await client.SendAsync(requestMessage);

            if(apiResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.Unauthorized,
                    ErrorMessage = "Not an authorized user!"
                };
            }

            var apiContent = await apiResponse.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
        }
    }
}
