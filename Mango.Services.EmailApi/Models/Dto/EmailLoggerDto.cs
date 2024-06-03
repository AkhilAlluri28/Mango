using System.ComponentModel.DataAnnotations;

namespace Mango.Services.EmailApi.Models.Dto
{
    public record EmailLoggerDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? EmailSentDate { get; set; }
    }
}
