namespace Mango.Services.AuthApi.Models
{
    /// <summary>
    /// Reponse Dto.
    /// </summary>
    public record ResponseDto
    {
        public bool IsSuccess { get; init; }
        public string? ErrorMessage { get; init; }
    }
}
