using Mango.Services.CartApi.Models.Dto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mango.Services.CartApi.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record CartDetails
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int CartDetailsId { get; init; }

        /// <summary>
        /// 
        /// </summary>
        public int CartHeaderId { get; init; }

        /// <summary>
        /// 
        /// </summary>

        [ForeignKey("CartHeaderId")]
        public CartHeader CartHeader { get; init; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; init; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public ProductDto Product { get; init; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public int Count { get; init; }
    }
}
