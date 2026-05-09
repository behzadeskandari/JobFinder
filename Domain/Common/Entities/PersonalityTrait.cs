using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace JobFinder.Domain.Common.Entities
{
    public class PersonalityTrait : IBaseEntity<Guid>
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } // e.g., Openness, Conscientiousness, Extraversion, Agreeableness, Neuroticism

        public string Description { get; set; }

        public DateTime? DateCreated { get; set; }

        public DateTime? DateModified { get; set; }

        public bool? IsActive { get; set; }

        // Navigation property
        [JsonIgnore]
        public ICollection<PersonalityTestItem> PersonalityTestItems { get; set; }
        public string TraitType { get; set; }
    }

}
