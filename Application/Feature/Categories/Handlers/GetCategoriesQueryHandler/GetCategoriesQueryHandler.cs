using AutoMapper;
using JobFinder.Application.Feature.Categories.Queries.GetCategories;
using JobFinder.Contracts.Dtos.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.Categories.Handlers.GetCategoriesQueryHandler
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryDto>>
    {
        //private readonly IApplicationDbContext _context;
        //private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetCategoriesQueryHandler(IUnitOfWork unitOfWork)
        {
            //_context = context;
            //_mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var record =  await _unitOfWork.CategoryRepository.GetAllAsync(cancellationToken);
            var category = record.Select(x => new CategoryDto
            {
                Description= x.Description,
                AdvertisementCount = record.Count(),
                Id = x.Id,
                Name = x.Name,
                
            }).ToList();
            return category;
        }
    }
}
