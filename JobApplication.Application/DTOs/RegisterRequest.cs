using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public UserType UserType { get; set; }
        public string? CompanyName { get; set; }
    }
}
