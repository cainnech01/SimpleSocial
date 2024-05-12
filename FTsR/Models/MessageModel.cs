using Microsoft.AspNetCore.Identity;

namespace FTsR.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string? Text { get; set; }
        public DateTime Timestamp { get; set; }

        public string FromUserId { get; set; }

        public string ToUserId { get; set; }
    }
}
