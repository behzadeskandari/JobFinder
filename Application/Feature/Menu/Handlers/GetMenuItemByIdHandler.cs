using AutoMapper;
using JobFinder.Application.Feature.Menu.Queries;
using JobFinder.Contracts.Dtos.Menu;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Repository;

namespace JobFinder.Application.Feature.Menu.Handlers
{
    public class GetMenuItemByIdHandler : IRequestHandler<GetMenuItemByIdQuery, MenuItemDto>
    {
        private readonly IMenuRepository _unitOfWork;
        private readonly IMapper _mapper;

        public GetMenuItemByIdHandler(IMenuRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MenuItemDto> Handle(GetMenuItemByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.GetByIdAsync(request.Id);
            if (entity == null)
                throw new NotFoundException(nameof(MenuItem), request.Id);

            return _mapper.Map<MenuItemDto>(entity);
        }
    }
}
