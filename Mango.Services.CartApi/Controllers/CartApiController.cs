﻿using AutoMapper;
using Mango.MessageBus;
using Mango.Services.CartApi.Data;
using Mango.Services.CartApi.Models;
using Mango.Services.CartApi.Models.Dto;
using Mango.Services.CartApi.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Mango.Services.CartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController(AppDbContext appDbContext,
                                    IMapper mapper, 
                                    IProductService productService,
                                    ICouponService couponService,
                                    IMessageBus messageBus,
                                    IConfiguration configuration) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        private readonly IProductService _productService = productService;
        private readonly ICouponService _couponService = couponService;
        private readonly IMessageBus _messageBus = messageBus;
        private readonly IConfiguration _configuration = configuration;

        [HttpGet]
        [Route("by-userId/{userId}")]
        public async Task<ResponseDto> GetCart(string userId)
        {
            try
            {
                CartHeader? cartHeaderFromDb = _appDbContext.CartHeaders.AsNoTracking().FirstOrDefault(c =>
                                                                               c.UserId == userId);
                if (cartHeaderFromDb is null)
                {
                    return new ResponseDto { ErrorMessage = "Cart not found", StatusCode = HttpStatusCode.NotFound };
                }

                IEnumerable<CartDetails> cartDetailsFromDb = _appDbContext.CartDetails.AsNoTracking()
                                                                  .Where(c =>
                                                                  c.CartHeaderId == cartHeaderFromDb.CartHeaderId);
                CartDto cartDto = new ()
                {
                    CartHeader = _mapper.Map<CartHeaderDto>(cartHeaderFromDb),
                    CartDetails = _mapper.Map<IEnumerable<CartDetailsDto>>(cartDetailsFromDb),
                };

                List<ProductDto> products = await _productService.GetAllProducts();

                foreach(var item in cartDto.CartDetails)
                {
                    item.Product = products.Find(p=> p.ProductId ==  item.ProductId);

                    if (item.Product is not null)
                        cartDto.CartHeader.CartTotal += item.Count * item.Product.Price;
                }

                // apply copon discount if any
                if (!string.IsNullOrWhiteSpace(cartDto.CartHeader.CouponCode))
                {
                    CouponDto couponDto = await _couponService.GetCouponByCode(cartDto.CartHeader.CouponCode);
                    if(CanCouponApplicable(cartDto, couponDto.MinAmount))
                    {
                        cartDto.CartHeader.CartTotal -= couponDto.DiscountAmount;
                        cartDto.CartHeader.Discount = couponDto.DiscountAmount;
                    }
                }

                return new ResponseDto { StatusCode = HttpStatusCode.OK, Body = cartDto };
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.InternalServerError };
            }
        }

        [HttpPost]
        [Route("cart-upsert")]
        public async Task<ResponseDto> CartUpsert([FromBody] CartDto cartDto)
        {
            if (!IsModelValid(cartDto))
                return new ResponseDto { ErrorMessage = "Payload is empty", StatusCode = HttpStatusCode.BadRequest };

            try
            {
                var cartHeaderFromDb = _appDbContext.CartHeaders.AsNoTracking().FirstOrDefault(c =>
                                                                               c.UserId == cartDto.CartHeader.UserId);
                if (cartHeaderFromDb is null)
                {
                    CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                    _appDbContext.CartHeaders.Add(cartHeader);
                    await _appDbContext.SaveChangesAsync();

                    cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;

                    _appDbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    var cartDetailsFromDb = await _appDbContext.CartDetails.AsNoTracking().FirstOrDefaultAsync(c =>
                        c.CartHeaderId == cartHeaderFromDb.CartHeaderId &&
                        c.ProductId == cartDto.CartDetails.First().ProductId);

                    if (cartDetailsFromDb is null)
                    {
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDb.CartHeaderId;

                        _appDbContext.CartDetails.Add(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                    else
                    {
                        cartDto.CartDetails.First().Count += cartDetailsFromDb.Count;
                        cartDto.CartDetails.First().CartHeaderId = cartDetailsFromDb.CartHeaderId;
                        cartDto.CartDetails.First().CartDetailsId = cartDetailsFromDb.CartDetailsId;

                        _appDbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.BadRequest };
            }

            return new ResponseDto { StatusCode = HttpStatusCode.OK, Body = cartDto };
        }

        [HttpDelete]
        [Route("remove-cart/{cartDetailsId:int}")]
        public async Task<ResponseDto> CartRemove(int cartDetailsId)
        {
            try
            {
                var cartDetailsFromDb = _appDbContext.CartDetails.AsNoTracking().FirstOrDefault(c =>
                                                                               c.CartDetailsId == cartDetailsId);
                if (cartDetailsFromDb is not null)
                {
                    int totalsCartDetailsCount = _appDbContext.CartDetails.Where(u => u.CartHeaderId == cartDetailsFromDb.CartHeaderId).Count();
                    _appDbContext.CartDetails.Remove(cartDetailsFromDb);
                    if (totalsCartDetailsCount == 1)
                    {
                        var cartHeaderToRemoveFromDb = await _appDbContext.CartHeaders
                                                                            .AsNoTracking()
                                                                            .FirstAsync(x =>
                                                                            x.CartHeaderId == cartDetailsFromDb.CartHeaderId);

                        _appDbContext.CartHeaders.Remove(cartHeaderToRemoveFromDb);
                    }
                    await _appDbContext.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.BadRequest };
            }

            return new ResponseDto { StatusCode = HttpStatusCode.NoContent };
        }

        [HttpPost]
        [Route("apply-coupon")]
        public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto cartDto)
        {
            if (!IsModelValid(cartDto))
                return new ResponseDto { ErrorMessage = "Payload is empty", StatusCode = HttpStatusCode.BadRequest };

            try
            {
                var cartHeaderFromDb = await _appDbContext.CartHeaders.AsNoTracking().FirstAsync(c =>
                                                                               c.UserId == cartDto.CartHeader.UserId);
                cartHeaderFromDb.CouponCode = cartDto.CartHeader.CouponCode;
                _appDbContext.CartHeaders.Update(cartHeaderFromDb);
                _appDbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.BadRequest };
            }

            return new ResponseDto { StatusCode = HttpStatusCode.OK, Body = true };
        }

        [HttpPost]
        [Route("email-cart")]
        public async Task<ResponseDto> EmailCart([FromBody] CartDto cartDto)
        {
            if (!IsModelValid(cartDto))
                return new ResponseDto { ErrorMessage = "Payload is empty", StatusCode = HttpStatusCode.BadRequest };

            try
            {
                string queue_topic_name = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
                await _messageBus.PublishMessage(cartDto, queue_topic_name);
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.BadRequest };
            }

            return new ResponseDto { StatusCode = HttpStatusCode.OK, Body = true };
        }

        private static bool IsModelValid(CartDto cartDto)
        {
            return cartDto?.CartHeader is not null;
        }

        private static bool CanCouponApplicable(CartDto cartDto, decimal minAmount)
        {
            return cartDto.CartHeader.CartTotal > minAmount;
        }
    }
}
