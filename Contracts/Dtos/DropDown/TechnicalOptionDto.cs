using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.DropDown
{
    public class TechnicalOptionDto
    {
        public int Id { get; set; }
        public string Label { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}
