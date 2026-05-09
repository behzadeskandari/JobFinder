using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Contracts.Dtos.Menu;
using MediatR;

namespace JobFinder.Application.Feature.Menu.Commands
{
    public record UpdateMenuItemCommand(int Id, string Title, string Url, int? ParentId, bool? IsActive, List<MenuItemDto> Children) : IRequest;


    //public class UpdateMenuItemCommand
    //{
    //    public int Id { get; set; }
    //    public string Title { get; set; }
    //    public string Url { get; set; }
    //    public int? ParentId { get; set; }
    //    public bool? IsActive { get; set; }
    //}
}
