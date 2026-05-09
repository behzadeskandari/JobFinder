using JobFinder.Contracts.Dtos.JobPost;
using JobFinder.Domain.Common.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Queries.GetJobPostsQuery
{
    public class GetJobPostsQuery : IRequest<PaginatedList<JobPostDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
        public string Status { get; set; }
    }
}
