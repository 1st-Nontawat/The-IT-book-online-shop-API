namespace TheITBookOnlineShop.Models.DTOs
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Fullname { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LikeBookRequest
    {
        public int UserId { get; set; }
        public string BookId { get; set; } = string.Empty;
    }
}