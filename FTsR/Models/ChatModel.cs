namespace FTsR.Models
{
    public class ChatModel
    {
        public string RecipientName { get; set; }
        public List<MessageModel> MyMessages { get; set; }
        public List<MessageModel> OtherMessages { get; set; }
        public MessageModel LastMessage { get; set; }
    }
}
