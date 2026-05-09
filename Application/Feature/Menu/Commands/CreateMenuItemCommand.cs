using JobFinder.Contracts.Dtos.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Menu.Commands
{
    //public class CreateMenuItemCommand
    //{
    //    public string Title { get; set; }
    //    public string Url { get; set; }
    //    public int? ParentId { get; set; }
    //    public bool? IsActive { get; set; }
    //}
    public record CreateMenuItemCommand(string Title, string Url, int? ParentId, bool? IsActive, List<MenuItemDto> Children) : IRequest<int>;
}
