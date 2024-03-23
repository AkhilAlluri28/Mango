using System.Net;

namespace Mango.Services.CouponApi.Common
{
    /// <summary>
    /// Common response model.
    /// </summary>
    public record ResponseDto
    {
        /// <summary>
        /// Status code of http call
        /// </summary>
        public HttpStatusCode StatusCode { get; init; } = HttpStatusCode.OK;

        /// <summary>
        /// Response body to consumer.
        /// </summary>
        public object? Body { get; init; }

        /// <summary>
        /// Error message
        /// </summary>
        public string? ErrorMessage { get; init; }

        /// <summary>
        /// Success or failed status.
        /// </summary>
        public bool IsSuccess
        {
            get
            {
                return (int)StatusCode >= 200 || (int)StatusCode <= 299;
            }
        }
    }
}
