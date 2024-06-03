namespace Mango.Services.EmailApi.Models
{
    public record EmailLogger
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? EmailSentDate { get; set; }
    }
}
