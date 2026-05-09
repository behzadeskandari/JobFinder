using JobFinder.Contracts.Dtos.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Menu.Queries
{
    public record GetMenuHierarchyQuery : IRequest<IEnumerable<MenuItemDto>>;
}
