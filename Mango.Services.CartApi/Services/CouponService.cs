using Mango.Services.CartApi.Models.Dto;
using Mango.Services.CartApi.Services.IServices;
using Newtonsoft.Json;

namespace Mango.Services.CartApi.Services
{
    public class CouponService(IHttpClientFactory httpClientFactory) : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        public async Task<List<CouponDto>> GetAllCoupons()
        {
            HttpClient client = _httpClientFactory.CreateClient("Coupon");

            HttpResponseMessage response = await client.GetAsync("/api/coupons");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                ResponseDto responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

                if (responseDto.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(responseDto.Body));
                }
            }
            return new List<CouponDto>();
        }

        public async Task<CouponDto> GetCouponByCode(string code)
        {
            HttpClient client = _httpClientFactory.CreateClient("Coupon");

            HttpResponseMessage response = await client.GetAsync($"/api/coupons/by-code/{code}");

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                ResponseDto responseDto = JsonConvert.DeserializeObject<ResponseDto>(content);

                if (responseDto.IsSuccess)
                {
                    return JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(responseDto.Body));
                }
            }
            return new CouponDto();
        }
    }
}
