using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int RecruiterId { get; set; }
        public Recruiter Recruiter { get; set; } = null!;

        public void Close()
        {
            if (!IsActive)
                throw new InvalidOperationException("Job is already closed.");

            IsActive = false;
        }
    }
}
