using AutoMapper;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Menu.Commands;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using JobFinder.Application.Repository;

namespace JobFinder.Application.Feature.Menu.Handlers
{
    public class UpdateMenuItemHandler : IRequestHandler<UpdateMenuItemCommand>
    {

        private readonly IMenuRepository _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMenuItemHandler(IMenuRepository unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task Handle(UpdateMenuItemCommand request, CancellationToken cancellationToken)
        {

            var entity = await _unitOfWork.FindAsync(request.Id);

            if (entity == null) throw new NotFoundException(nameof(MenuItem), request.Id);

            entity.Title = request.Title;
            entity.Url = request.Url;
            entity.ParentId = request.ParentId;
            entity.IsActive = request.IsActive;
            entity.DateModified = DateTime.Now;
            _unitOfWork.save();
              
        }
    }
}
