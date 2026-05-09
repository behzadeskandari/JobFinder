using AutoMapper;
using JobFinder.Application.Feature.Menu.Commands;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Repository;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Menu.Handlers
{
    public class CreateMenuItemHandler : IRequestHandler<CreateMenuItemCommand, int>
    {
        private readonly IMenuRepository _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMenuItemHandler(IMenuRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMenuItemCommand request, CancellationToken cancellationToken)
        {
            var entity = new MenuItem
            {
                Title = request.Title,
                Url = request.Url,
                ParentId = request.ParentId,
                IsActive = request.IsActive,
                DateCreated = DateTime.Now
            };

            await _unitOfWork.AddAsync(entity);
            _unitOfWork.save();
            return entity.Id;
        }
    }
}
