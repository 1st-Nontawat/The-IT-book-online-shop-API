namespace TheITBookOnlineShop.Models.DTOs
{
    public class GoogleBooksResponse
    {
       
        public List<GoogleBookItem>? Items { get; set; }
    }

    public class GoogleBookItem
    {
        
        public VolumeInfo? VolumeInfo { get; set; }
    }

    public class VolumeInfo
    {
        
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public List<string>? Authors { get; set; }

    
        public int PageCount { get; set; }
    }
}