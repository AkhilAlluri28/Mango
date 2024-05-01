using AutoMapper;
using Mango.Services.CouponApi.Common;
using Mango.Services.CouponApi.Interfaces;
using Mango.Services.CouponApi.Models;
using Mango.Services.CouponApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mango.Services.CouponApi.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    [Authorize]
    public class CouponAPIController(ICouponRepository couponRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICouponRepository _couponRepository = couponRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        public ResponseDto GetAllCoupons()
        {
            return new ResponseDto
            {
                Body = _mapper.Map<IEnumerable<CouponDto>>(_couponRepository.GetAllCoupons())
            };
        }

        [HttpGet]
        [Route("{couponId:int}")]
        public ResponseDto GetCouponById(int couponId)
        {
            Coupon? couponById = _couponRepository.GetCouponById(couponId);
            return new ResponseDto
            {
                StatusCode = couponById == null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Body = _mapper.Map<CouponDto>(couponById)
            };
        }

        [HttpGet]
        [Route("by-code/{couponCode}")]
        public ResponseDto GetCouponByCode(string couponCode)
        {
            Coupon? couponByCode = _couponRepository.GetCouponByCode(couponCode);
            return new ResponseDto
            {
                StatusCode = couponByCode == null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Body = _mapper.Map<CouponDto>(couponByCode)
            };
        }

        [HttpPost]
        public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
        {
            Coupon couponToCreate = _mapper.Map<Coupon>(couponDto);
            _couponRepository.CreateCoupon(couponToCreate);
            return new ResponseDto
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        [HttpPut]
        [Route("{couponId:int}")]
        public ResponseDto UpdateCoupon(int couponId, [FromBody] CouponDto couponDto)
        {
            if (couponId != couponDto.CouponId)
                return new ResponseDto { StatusCode = HttpStatusCode.BadRequest };

            try
            {
                Coupon couponToUpdate = _mapper.Map<Coupon>(couponDto);
                _couponRepository.UpdateCoupon(couponToUpdate);
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Body = couponDto
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
        [Route("{couponId:int}")]
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
