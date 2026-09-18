using JobApplication.Application.DTOs;
using JobApplication.Application.Interfaces;
using JobApplication.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Application.Services
{
    public class JobService
    {
        private readonly IJobRepository _jobRepository;
        private readonly ICurrentUserService _currentUserService;

        public JobService(IJobRepository jobRepository, ICurrentUserService currentUserService)
        {
            _jobRepository = jobRepository;
            _currentUserService = currentUserService;
        }

        public async Task<int> CreateAsync(CreateJobDto createJobDto)
        {   
            var job = new Job()
            {
                Title = createJobDto.Title,
                Description = createJobDto.Description,
                IsActive = true,
                RecruiterId = _currentUserService.UserId
            };
            await _jobRepository.InsertAsync(job);
            await _jobRepository.SaveChangesAsync();

            return job.Id; 
        }

        public async Task CloseAsync(int jobId)
        {
            var job = await _jobRepository.GetByIdAsync(jobId);
            if (job == null)
                throw new KeyNotFoundException("Job not found.");

            if (job.RecruiterId != _currentUserService.UserId)
                throw new UnauthorizedAccessException("You do not own this job.");

            job.Close();

            _jobRepository.Update(job);
            await _jobRepository.SaveChangesAsync();
        }
    }
}
