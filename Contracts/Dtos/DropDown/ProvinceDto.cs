using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.DropDown
{
    public class ProvinceDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public string Label { get; set; }
        public List<CityDto>? Cities { get; set; }
        public bool IsActive { get; set; }
    }
}
