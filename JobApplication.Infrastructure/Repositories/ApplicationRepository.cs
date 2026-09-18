using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace JobApplication.Infrastructure.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly ApplicationDbContext _context;

        public ApplicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<JobCandidateApplication?> GetByIdAsync(int id)
        {
            return await _context.JobCandidateApplications.FindAsync(id);
        }

        public async Task InsertAsync(JobCandidateApplication application)
        {
            await _context.JobCandidateApplications.AddAsync(application);
        }

        public async Task<bool> ExistsAsync(int candidateId, int jobId)
        {
            return await _context.JobCandidateApplications
                .AnyAsync(a => a.CandidateId == candidateId && a.JobId == jobId);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
