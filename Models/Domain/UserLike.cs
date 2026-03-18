using System.ComponentModel.DataAnnotations;

namespace TheITBookOnlineShop.Models.Entities
{
    public class UserLike
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string BookId { get; set; } = string.Empty;

        public DateTime LikedAt { get; set; } = DateTime.UtcNow;
    }
}