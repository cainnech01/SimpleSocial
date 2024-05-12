namespace FTsR.Models
{
    public class CreatePinModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile AuthorImage { get; set; }
        public IFormFile Image { get; set; }
    }
}
