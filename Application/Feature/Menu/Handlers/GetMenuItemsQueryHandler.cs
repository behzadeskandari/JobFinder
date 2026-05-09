using JobFinder.Application.Feature.Menu.Queries.GetMenuItems;
using JobFinder.Contracts.Dtos.Menu;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Repository;

namespace JobFinder.Application.Feature.Menu.Handlers
{
    // Application/MenuItems/Queries/GetMenuItemsQueryHandler.cs
    public class GetMenuItemsQueryHandler : IRequestHandler<GetMenuItemsQuery, List<MenuItemDto>>
    {
        private readonly IMenuRepository _unitOfWork;

        public GetMenuItemsQueryHandler(IMenuRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MenuItemDto>> Handle(GetMenuItemsQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.GetMenuWithChildren(cancellationToken);
        }
    }
}
