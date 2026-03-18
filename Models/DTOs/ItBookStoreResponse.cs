namespace TheITBookOnlineShop.Models.DTOs
{
    public class ItBookStoreResponse
    {
        public string Error { get; set; } = string.Empty;
        public string Total { get; set; } = string.Empty;
        public string Page { get; set; } = string.Empty;
        public List<BookDto> Books { get; set; } = new List<BookDto>();
    }

    public class BookDto
    {
        public string Title { get; set; } = string.Empty;
        public string Subtitle { get; set; } = string.Empty;
        public string Isbn13 { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}