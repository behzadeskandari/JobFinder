using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobPosts.Queries.GetJobPostsByStaffIdQuery;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.JobPosts.Handler.GetJobPostsByStaffIdHandler
{
    public class GetJobPostsByStaffIdHandler : IRequestHandler<GetJobPostsByStaffIdQuery, Result<List<JobPost>>>
    {
        private readonly IUnitOfWork _context;

        public GetJobPostsByStaffIdHandler(IUnitOfWork context)
        {
            _context = context;
        }

        public async Task<Result<List<JobPost>>> Handle(GetJobPostsByStaffIdQuery request, CancellationToken cancellationToken)
        {
            var jobPosts = await _context.JobPostsRepository.GetQueryable().Where(jp => jp.StaffId == request.StaffId).ToListAsync(cancellationToken);
            if (jobPosts == null || jobPosts.Count == 0)
            {
                throw new NotFoundException($"هیچ آگهی شغلی برای StaffId {request.StaffId} یافت نشد.");;
            }
            return Result.Ok(jobPosts);
        }
    }
}
