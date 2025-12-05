namespace ShorterLinks.Server.DTOs
{
    public class AuthResultDto
    {
        public string? Token { get; set; }
        public UserDto? User { get; set; }
    }
}
