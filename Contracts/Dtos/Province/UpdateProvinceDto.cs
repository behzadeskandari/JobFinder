using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Province
{
    public class UpdateProvinceDto
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public bool? IsActive { get; set; }
    }
}
