using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces
{
    public interface IApplicationRepository
    {
        Task<JobCandidateApplication?> GetByIdAsync(int id);
        Task InsertAsync(JobCandidateApplication application);
        Task<bool> ExistsAsync(int candidateId, int jobId);
        Task SaveChangesAsync();
    }
}
