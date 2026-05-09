using AutoMapper;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobPosts.Queries.GetJobPostById;
using JobFinder.Contracts.Dtos.JobPost;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Handler.GetJobPostByIdQueryHandler
{
    public class GetJobPostByIdQueryHandler : IRequestHandler<GetJobPostByIdQuery, JobPostDto>
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetJobPostByIdQueryHandler(IUnitOfWork context, IMapper mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<JobPostDto> Handle(GetJobPostByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.JobPostsRepository.GetQueryable()
                .Include(jp => jp.Staff)
                .FirstOrDefaultAsync(jp => jp.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(JobPost), request.Id);
            }

            // If user is staff, only allow them to view their own job posts
            if (_currentUserService.IsInRole("Staff") && !_currentUserService.IsInRole("Admin") &&
                entity.StaffId != _currentUserService.UserId)
            {
                throw new ForbiddenAccessException();
            }

            return _mapper.Map<JobPostDto>(entity);
        }
    }
}
