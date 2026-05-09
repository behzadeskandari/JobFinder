using AutoMapper;
using JobFinder.Application.Feature.Menu.Queries;
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
    public class GetMenuHierarchyHandler : IRequestHandler<GetMenuHierarchyQuery, IEnumerable<MenuItemDto>>
    {
        private readonly IMenuRepository _unitOfWork;
        private readonly IMapper _mapper;

        public GetMenuHierarchyHandler(IMenuRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> Handle(GetMenuHierarchyQuery request, CancellationToken cancellationToken)
        {
            var entities = await _unitOfWork.GetRootMenuItemsAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(entities);
        }
    }
}
