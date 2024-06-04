namespace FTsR.Models
{
    public class Pin
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[]? AuthorProfileImage { get; set; }
        public byte[] Image { get; set; }
    }

    public class SavedPinModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
