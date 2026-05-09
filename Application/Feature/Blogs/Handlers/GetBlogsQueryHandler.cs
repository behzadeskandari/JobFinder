using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Blogs.Queries;
using JobFinder.Contracts.Dtos.Blogs;
using JobFinder.Domain.Common.Entities;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Blogs.Handlers
{
    public class GetBlogsQueryHandler : IRequestHandler<GetBlogsQuery, Result<PagedResult<BlogDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<BlogDto>>> Handle(GetBlogsQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.BlogRepository
                .GetQueryable()
                .Where(b => b.IsActive == true);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(b => b.Title.ToLower().Contains(searchTerm) || b.Content.ToLower().Contains(searchTerm));
            }

            var totalCount = await query.CountAsync(cancellationToken);

            var blogs = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            var blogDtos = _mapper.Map<IEnumerable<BlogDto>>(blogs);

            var result = new PagedResult<BlogDto>
            {
                Items = blogDtos,
                TotalItems = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result.Ok(result);
        }
    }
}
