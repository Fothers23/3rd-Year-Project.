using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class AvailableJobViewModel
    {
        public int NumberOfJobs { get; set; }
        public List<JobSubmissionModel> Jobs { get; set; }
    }
}
