namespace FTsR.Models
{
    public class PinModel
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? AuthorProfileImage { get; set; }
        public byte[] Image { get; set; }
    }
}
