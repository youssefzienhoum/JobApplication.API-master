using JobApplication.Domain.Enums;

namespace JobApplication.Application.DTOs
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public UserType UserType { get; set; }
    }
}
