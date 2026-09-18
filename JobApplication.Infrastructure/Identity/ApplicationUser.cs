using JobApplication.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public UserType UserType { get; set; }
    }
}
