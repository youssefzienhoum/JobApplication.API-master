using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces
{
    public interface ICandidateRepository
    {
        Task<Candidate?> GetByUserIdAsync(string userId);
        Task InsertAsync(Candidate candidate);
        Task SaveChangesAsync();
    }
}
