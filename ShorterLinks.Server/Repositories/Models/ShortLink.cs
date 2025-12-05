namespace ShorterLinks.Server.Repositories.Models
{
    public class ShortLink
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        public int ClickCount {  get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastAccessed { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string ShortUrl => $"http://localhost:5186/m/{ShortCode}";

    }
}
