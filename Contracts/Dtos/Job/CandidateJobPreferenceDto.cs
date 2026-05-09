using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Contracts.Dtos.Account;

namespace JobFinder.Contracts.Dtos.Job
{
    public class CandidateJobPreferenceDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string PreferredJobType { get; set; }
        public string PreferredLocation { get; set; }
        public string PreferredIndustry { get; set; }
        public decimal? ExpectedSalary { get; set; }
        // Add other preference fields as needed
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }
        public int CityId { get; set; }
        public int? JobCategoryId { get; set; }
        public string JobType { get; set; }
        public decimal? MinSalary { get; set; }

    }
}
