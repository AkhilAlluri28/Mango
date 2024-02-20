using Mango.Services.CouponApi.Common;
using Mango.Services.CouponApi.Interfaces;
using Mango.Services.CouponApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponAPIController(ICouponRepository couponRepository) : ControllerBase
    {
        private readonly ICouponRepository _couponRepository = couponRepository;

        [HttpGet]
        [Route("coupons")]
        public ResponseDto GetAllCoupons()
        {
            return new ResponseDto
            {
                Body = _couponRepository.GetAllCoupons()
            };
        }

        [HttpGet]
        [Route("coupons/ids/{couponId:int}")]
        public ResponseDto GetCouponById(int couponId)
        {
            Coupon? couponById = _couponRepository.GetCouponById(couponId);
            return new ResponseDto
            {
                StatusCode = couponById == null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Body = couponById
            };
        }

        [HttpGet]
        [Route("coupons/codes/{couponCode}")]
        public ResponseDto GetCouponByCode(string couponCode)
        {
            Coupon? couponByCode = _couponRepository.GetCouponByCode(couponCode);
            return new ResponseDto
            {
                StatusCode = couponByCode == null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Body = couponByCode
            };
        }

        [HttpPost]
        [Route("coupons")]
        public ResponseDto CreateCoupon([FromBody] Coupon coupon)
        {
            _couponRepository.CreateCoupon(coupon);
            return new ResponseDto
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        [HttpPut]
        [Route("coupons/{couponId:int}")]
        public ResponseDto UpdateCoupon(int couponId, [FromBody] Coupon coupon)
        {
            if (couponId != coupon.CouponId)
                return new ResponseDto { StatusCode = HttpStatusCode.BadRequest };

            try
            {
                _couponRepository.UpdateCoupon(coupon);
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Body = coupon
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessage = ex.Message
                };
            }
        }

        [HttpDelete]
        [Route("coupons/{couponId:int}")]
        public ResponseDto DeleteCoupon(int couponId)
        {
            try
            {
                _couponRepository.DeleteCoupon(couponId);

                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NoContent,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NotFound,
                    ErrorMessage = ex.Message
                };
            }
        }
    }
}
