namespace ShorterLinks.Server.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
