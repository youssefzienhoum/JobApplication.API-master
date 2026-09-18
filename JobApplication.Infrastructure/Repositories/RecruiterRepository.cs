using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class RecruiterRepository : IRecruiterRepository
    {
        private readonly ApplicationDbContext _context;

        public RecruiterRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Recruiter?> GetByUserIdAsync(string userId)
        {
            return await _context.Recruiters.FirstOrDefaultAsync(r => r.ApplicationUserId == userId);
        }

        public async Task InsertAsync(Recruiter recruiter)
        {
            await _context.Recruiters.AddAsync(recruiter);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
