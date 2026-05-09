using AutoMapper;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Feature.JobPosts.Queries.GetJobPostsQuery;
using JobFinder.Contracts.Dtos.JobPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;

namespace JobFinder.Application.Feature.JobPosts.Handler.GetJobPostsQueryHandler
{
    public class GetJobPostsQueryHandler : IRequestHandler<GetJobPostsQuery, PaginatedList<JobPostDto>>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetJobPostsQueryHandler(IUnitOfWork context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginatedList<JobPostDto>> Handle(GetJobPostsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.JobPostsRepository.GetQueryable()  
                .Include(jp => jp.Staff)
                .AsQueryable();

            // Filter by search term
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(jp =>
                    jp.Title.ToLower().Contains(searchTerm) ||
                    jp.Description.ToLower().Contains(searchTerm) ||
                    jp.Location.ToLower().Contains(searchTerm));
            }

            // Filter by status
            if (!string.IsNullOrEmpty(request.Status))
            {
                if (request.Status.ToLower() == "active")
                {
                    query = query.Where(jp => jp.IsActive!.Value);
                }
                else if (request.Status.ToLower() == "inactive")
                {
                    query = query.Where(jp => !jp.IsActive!.Value);
                }
            }

            // If user is staff, only show their job posts
            if (_currentUserService.IsInRole("Staff") && !_currentUserService.IsInRole("Admin"))
            {
                query = query.Where(jp => jp.StaffId == _currentUserService.UserId);
            }

            var jobPosts = await query
                .OrderByDescending(jp => jp.CreatedAt)
                .ProjectTo<JobPostDto>(_mapper.ConfigurationProvider)
                .Take(request.PageSize).Skip(request.PageNumber).ToListAsync();

            PaginatedList<JobPostDto> paginatedList = new PaginatedList<JobPostDto>(jobPosts,
                await query.CountAsync(),
                request.PageNumber, 
                request.PageSize,
                totalPages: (int)Math.Ceiling((double)await query.CountAsync() / request.PageSize)
                );
            //{
            //    Items = jobPosts.AsEnumerable(),
            //    TotalCount = await query.CountAsync(),
            //    PageSize = request.PageSize,
            //    PageNumber = request.PageNumber
            //};  
            return paginatedList;
        }
    }
}
