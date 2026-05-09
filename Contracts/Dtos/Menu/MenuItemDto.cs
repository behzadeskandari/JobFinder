using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.Menu
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public int? ParentId { get; set; }
        public List<MenuItemDto> Children { get; set; } = new List<MenuItemDto>();
        public bool? IsActive { get; set; }
    }
}
