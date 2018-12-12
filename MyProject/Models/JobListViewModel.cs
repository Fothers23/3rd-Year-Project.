using System.Collections.Generic;

namespace MyProject.Models
{
    public class JobListViewModel
    {
        public int NumberOfJobs { get; set; }
        public List<JobSubmissionModel> Jobs { get; set; }
    }
}
