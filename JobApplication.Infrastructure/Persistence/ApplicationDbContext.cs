using JobApplication.Domain.Entities;
using JobApplication.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace JobApplication.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<JobCandidateApplication> JobCandidateApplications { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Job>(entity =>
            {
                entity.HasIndex(j => j.RecruiterId);
            });

            builder.Entity<Candidate>(entity =>
            {
                entity.HasIndex(c => c.UserId).IsUnique();
            });

            builder.Entity<JobCandidateApplication>(entity =>
            {
                entity.HasIndex(a => new { a.CandidateId, a.JobId }).IsUnique();
            });
        }
    }
}
