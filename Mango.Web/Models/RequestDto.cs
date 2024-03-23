namespace Mango.Web.Models
{
    /// <summary>
    ///  Http Request Dto.
    /// </summary>
    public record RequestDto
    {
        /// <summary>
        /// Api type - GET/POST/PUT/PATCH/DELETE etc.
        /// </summary>
        public HttpMethod Method { get; init; } = HttpMethod.Get;

        /// <summary>
        /// Api address.
        /// </summary>
        public string Url { get; init; } = null!;

        /// <summary>
        /// Request payload.
        /// </summary>
        public object? Body { get; init; }
    }
}
