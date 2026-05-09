using JobFinder.Contracts.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace JobFinder.Domain.Common.Entities
{
    public class Language : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Resume")]
        public Guid ResumeId { get; set; }
        public virtual Resume Resume { get; set; } = new Resume();

        [Required]
        public string Name { get; set; }
        [Required]
        public ProficiencyLevelEnum ProficiencyLevel { get; set; } //
        public DateTime? DateCreated { get ; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsActive { get; set; }

    }
}
