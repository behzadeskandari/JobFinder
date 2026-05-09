using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Candidate
{
    public record CandidateSearchCriteria
    {
        public CandidateSearchCriteria()
        {
            
        }
        public CandidateSearchCriteria(string? firstName, string? lastName, string? email, int? yearsOfExperience, Guid? educationLevelId, int? cityId, string? mBTIType, List<int>? skillIds, bool? isActive, int pageIndex, int pageSize, string? sortBy, string? sortOrder, DateTime? appliedDateFrom, DateTime? appliedDateTo, string? keywordInCoverLetter)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            YearsOfExperience = yearsOfExperience;
            EducationLevelId = educationLevelId;
            CityId = cityId;
            MBTIType = mBTIType;
            SkillIds = skillIds;
            IsActive = isActive;
            PageIndex = pageIndex;
            PageSize = pageSize;
            SortBy = sortBy;
            SortOrder = sortOrder;
            AppliedDateFrom = appliedDateFrom;
            AppliedDateTo = appliedDateTo;
            KeywordInCoverLetter = keywordInCoverLetter;
        }

        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public int? YearsOfExperience { get; set; }
        public Guid? EducationLevelId { get; set; }
        public int? CityId { get; set; }
        public string? MBTIType { get; set; }
        public List<int>? SkillIds { get; set; }
        public bool? IsActive { get; set; }

        // **Pagination**
        public int PageIndex { get; set; } = 0; // Current page number (0-indexed)
        public int PageSize { get; set; } = 10; // Number of items per page

        // **Sorting**
        public string? SortBy { get; set; } // Field to sort by, e.g., "DateCreated", "LastName"
        public string? SortOrder { get; set; } // "asc" for ascending, "desc" for descending

        // **Advanced Filtering (Optional additions)**
        public DateTime? AppliedDateFrom { get; set; }
        public DateTime? AppliedDateTo { get; set; }
        public string? KeywordInCoverLetter { get; set; } // For full-text search

        // Constructor for default values or specific scenarios
       
    }
}
