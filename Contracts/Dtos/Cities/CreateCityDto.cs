using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Cities
{
    public class CreateCityDto
    {
        public string Label { get; set; } = string.Empty;
        public int ProvinceId { get; set; }
        public bool? IsActive { get; set; }
        public string Value { get; set; }
    }
}
