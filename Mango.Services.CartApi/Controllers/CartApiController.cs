using AutoMapper;
using Mango.Services.CartApi.Data;
using Mango.Services.CartApi.Models;
using Mango.Services.CartApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Mango.Services.CartApi.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartApiController(AppDbContext appDbContext, IMapper mapper) : ControllerBase
    {
        private readonly AppDbContext _appDbContext = appDbContext;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        public async Task<ResponseDto> CartUpsert([FromBody]CartDto cartDto)
        {
            if (cartDto is null) 
                return new ResponseDto { ErrorMessage = "Payload is empty", StatusCode = HttpStatusCode.BadRequest };
           
            try
            {
                var cartHeaderFromDb = _appDbContext.CartHeaders.AsNoTracking().FirstOrDefault(c =>
                                                                               c.UserId == cartDto.CartHeader.UserId);
                if(cartHeaderFromDb is null)
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

                    if(cartDetailsFromDb is null)
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
    }
}
