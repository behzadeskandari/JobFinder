using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.PersonalityTestItem;

namespace JobFinder.Contracts.Dtos.PersonalityTrait
{
    public class PersonalityTraitDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } // e.g., Openness, Conscientiousness, Extraversion, Agreeableness, Neuroticism

        public string Description { get; set; }
        public bool? IsActive { get; set; }

        // Navigation property
        public ICollection<PersonalityTestItemDto> PersonalityTestItems { get; set; }
    }
}
