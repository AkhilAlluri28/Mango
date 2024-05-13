using AutoMapper;
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
    public class CartApiController(AppDbContext appDbContext, IMapper mapper, IProductService productService) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;
        public readonly IProductService _productService = productService;

        [HttpPost]
        [Route("{userId}")]
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
            if (cartDto is null)
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

        [HttpPost]
        [Route("remove-cart")]
        public async Task<ResponseDto> CartRemove([FromBody] int cartDetailsId)
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
                        await _appDbContext.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto { ErrorMessage = ex.Message, StatusCode = HttpStatusCode.BadRequest };
            }

            return new ResponseDto { StatusCode = HttpStatusCode.NoContent };
        }
    }
}
