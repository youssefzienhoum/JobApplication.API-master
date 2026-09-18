using System;
using System.Collections.Generic;
using System.Text;

namespace JobApplication.Domain.Entities
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description  { get; set; }
        public bool IsActive { get; set; }
        public string RecruiterId { get; set; }

        public void Close()
        {
            if (!IsActive)
                throw new InvalidOperationException("Job is already closed.");

            IsActive = false;
        }
    }
}
