using AutoMapper;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Feature.Categories.Queries.GetCategoryByIdQuery;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Categories.Handlers.GetCategoryByIdQueryHandler
{

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        //private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            //_context = context;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.CategoryRepository.GetByIdAsyncWithAdvertisements(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Category), request.Id);
            }

            var dto = _mapper.Map<CategoryDto>(entity);
            dto.AdvertisementCount = entity.Advertisements.Count;
            return dto;
        }
    }
}
