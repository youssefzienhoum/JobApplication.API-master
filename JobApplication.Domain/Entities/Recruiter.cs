using System.Collections.Generic;

namespace JobApplication.Domain.Entities
{
    public class Recruiter
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public ICollection<Job> Jobs { get; set; } = new List<Job>();
    }
}
