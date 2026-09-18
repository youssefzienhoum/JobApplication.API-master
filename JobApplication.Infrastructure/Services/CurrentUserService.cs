using JobApplication.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace JobApplication.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId =>
            _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

        public bool IsAuthenticated =>
            _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;

        public bool IsRecruiter =>
            _httpContextAccessor.HttpContext?.User?.IsInRole("Recruiter") ?? false;

        public bool IsCandidate =>
            _httpContextAccessor.HttpContext?.User?.IsInRole("Candidate") ?? false;
    }
}
