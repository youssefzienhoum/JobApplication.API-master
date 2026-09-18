using JobApplication.Domain.Entities;

namespace JobApplication.Application.Interfaces
{
    public interface IRecruiterRepository
    {
        Task<Recruiter?> GetByUserIdAsync(string userId);
        Task InsertAsync(Recruiter recruiter);
        Task SaveChangesAsync();
    }
}
