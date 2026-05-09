using Domain.Roles;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public override string? Email { get; set; }
        public string Password { get; set; }  = string.Empty;
        public bool? IsActive { get; set; }
        public string PictureUrl { get; set; } = string.Empty;
        public string Role { get; set; } = Roles.Role_User;

        public string RefreshToken { get; set; } = string.Empty;
        public DateTime RefreshTokenExpiryTime { get; set; }

        public DateTime? DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateModified { get; set; } = DateTime.Now;
    
        //public int JobOffersId { get; set; }

        //public int JobPostId { get; set; }
        // Navigation properties
        public ICollection<Advertisement> Advertisements { get; set; }
        public ICollection<Candidate> Candidates { get; set; }
        public ICollection<JobPost> JobPosts { get; set; }
        public ICollection<JobRequest> JobRequests { get; set; }
        public ICollection<Job> Jobs { get; set; }
        public MBTIResult MBTIResult { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Payment> Payments { get; set; }
        public ICollection<Resume> Resumes { get; set; }
        public ICollection<PsychologyTestResponse> PsychologyTestResponses { get; set; }
        public ICollection<PsychologyTestResult> PsychologyTestResults { get; set; }
        public ICollection<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        public ICollection<PersonalityTestResult> PersonalityTestResults { get; set; }

        [NotMapped]
        public string RedirectUrl { get; set; }
        ////////////////////////////////////////////////////
        // Navigation properties
        //public ICollection<JobPost> JobPosts { get; set; } = new List<JobPost>();
        //public ICollection<JobRequest> JobRequests { get; set; } = new List<JobRequest>();
        //public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
        //public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        //public ICollection<Order> Orders { get; set; } = new List<Order>();
        //public ICollection<Payment> Payments { get; set; } = new List<Payment>();
        //public ICollection<Resume> Resumes { get; set; } = new List<Resume>();
        //public ICollection<MBTIResult> MBTIResults { get; set; } = new List<MBTIResult>();
        /////////////////////////////////////////////////////////////////////////////
        //public ICollection<JobRequest> JobRequests { get; set; } = new List<JobRequest>();
        //public ICollection<JobOffer> JobOffers { get; set; } = new List<JobOffer>();
        //public ICollection<Advertisement> Advertisements { get; set; } = new List<Advertisement>();
        //public ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }



    public class AspNetRoleClaim : IdentityRoleClaim<string>
    {
        // No additional properties needed based on the SQL script
    }
    public class AspNetRole : IdentityRole
    {
        // No additional properties needed based on the SQL script
    }

    public class AspNetUserClaim : IdentityUserClaim<string>
    {
        // No additional properties needed based on the SQL script
    }

    public class AspNetUserLogin : IdentityUserLogin<string>
    {
        // No additional properties needed based on the SQL script
    }

    public class AspNetUserRole : IdentityUserRole<string>
    {
        // No additional properties needed based on the SQL script
    }
    public class AspNetUserToken : IdentityUserToken<string>
    {
        // No additional properties needed based on the SQL script
    }
}
