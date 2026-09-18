using JobApplication.Application.DTOs;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;
using JobApplication.Infrastructure.Identity;
using JobApplication.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace JobApplication.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly ApplicationDbContext _context;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _context = context;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                throw new InvalidOperationException("A user with this email already exists.");

            var user = new ApplicationUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                UserType = request.UserType
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new InvalidOperationException(errors);
            }

            var roleName = request.UserType.ToString();
            await _userManager.AddToRoleAsync(user, roleName);

            if (request.UserType == UserType.Candidate)
            {
                var candidate = new Candidate
                {
                    Name = request.FullName,
                    CvUrl = string.Empty,
                    UserId = user.Id
                };
                await _context.Candidates.AddAsync(candidate);
                await _context.SaveChangesAsync();
            }

            var token = _tokenService.GenerateToken(user.Id, user.Email!, roleName);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email!,
                Role = roleName
            };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                throw new InvalidOperationException("Invalid email or password.");

            var validPassword = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!validPassword)
                throw new InvalidOperationException("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? user.UserType.ToString();

            var token = _tokenService.GenerateToken(user.Id, user.Email!, role);

            return new AuthResponse
            {
                Token = token,
                Email = user.Email!,
                Role = role
            };
        }
    }
}
