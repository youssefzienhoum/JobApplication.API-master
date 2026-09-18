using JobApplication.Application.DTOs;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using JobApplication.Domain.Enums;

namespace JobApplication.Application.Services
{
    public class ApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IJobRepository _jobRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly ICurrentUserService _currentUserService;

        public ApplicationService(
            IApplicationRepository applicationRepository,
            IJobRepository jobRepository,
            ICandidateRepository candidateRepository,
            ICurrentUserService currentUserService)
        {
            _applicationRepository = applicationRepository;
            _jobRepository = jobRepository;
            _candidateRepository = candidateRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> ApplyAsync(ApplyJobRequest request)
        {
            var job = await _jobRepository.GetByIdAsync(request.JobId);
            if (job == null)
                throw new KeyNotFoundException("Job not found.");

            if (!job.IsActive)
                throw new InvalidOperationException("Cannot apply to a closed job.");

            var candidate = await _candidateRepository.GetByUserIdAsync(_currentUserService.UserId);
            if (candidate == null)
                throw new UnauthorizedAccessException("Candidate profile not found.");

            var alreadyApplied = await _applicationRepository.ExistsAsync(candidate.Id, request.JobId);
            if (alreadyApplied)
                throw new InvalidOperationException("You have already applied to this job.");

            var application = new JobCandidateApplication
            {
                CandidateId = candidate.Id,
                JobId = request.JobId,
                JobApplicationStatus = JobApplicationStatus.Applied,
                AppliedAt = DateTime.UtcNow,
                StatusUpdatedAt = DateTime.UtcNow
            };

            await _applicationRepository.InsertAsync(application);
            await _applicationRepository.SaveChangesAsync();

            return application.Id;
        }

        public async Task CancelAsync(int applicationId)
        {
            var application = await _applicationRepository.GetByIdAsync(applicationId);
            if (application == null)
                throw new KeyNotFoundException("Application not found.");

            var candidate = await _candidateRepository.GetByUserIdAsync(_currentUserService.UserId);
            if (candidate == null || application.CandidateId != candidate.Id)
                throw new UnauthorizedAccessException("You do not own this application.");

            application.Cancel();

            await _applicationRepository.SaveChangesAsync();
        }
    }
}
