using AutoMapper;
using Mango.Services.ProductApi.Common;
using Mango.Services.ProductApi.Interfaces;
using Mango.Services.ProductApi.Models;
using Mango.Services.ProductApi.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Mango.Services.ProductApi.Controllers
{

    [Route("api/products")]
    [ApiController]
    public class ProductAPIController(IProductRepository productRepository, IMapper mapper) : ControllerBase
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
		public ResponseDto GetAllProducts()
        {
            return new ResponseDto
            {
                Body = _mapper.Map<IEnumerable<ProductDto>>(_productRepository.GetAllProducts())
            };
        }

        [HttpGet]
        [Route("{productId:int}")]
        public ResponseDto GetProductById(int productId)
        {
            Product? product = _productRepository.GetProductById(productId);
            return new ResponseDto
            {
                StatusCode = product == null ? HttpStatusCode.NotFound : HttpStatusCode.OK,
                Body = _mapper.Map<ProductDto>(product)
            };
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto CreateProduct([FromBody] ProductDto productDto)
        {
            Product productToCreate = _mapper.Map<Product>(productDto);
            _productRepository.Create(productToCreate);
            return new ResponseDto
            {
                StatusCode = HttpStatusCode.NoContent
            };
        }

        [HttpPut]
        [Route("{productId:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto UpdateProduct(int productId, [FromBody] ProductDto productDto)
        {
            if (productId != productDto.ProductId)
                return new ResponseDto { StatusCode = HttpStatusCode.BadRequest };

            try
            {
                Product productToUpdate = _mapper.Map<Product>(productDto);
                _productRepository.Update(productToUpdate);
                return new ResponseDto
                {
                    StatusCode = HttpStatusCode.NoContent,
                    Body = productDto
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
        [Route("{productId:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto DeleteProduct(int productId)
        {
            try
            {
                _productRepository.Delete(productId);

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
