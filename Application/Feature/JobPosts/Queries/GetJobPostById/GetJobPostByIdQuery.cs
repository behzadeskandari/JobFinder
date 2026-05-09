using JobFinder.Contracts.Dtos.JobPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Queries.GetJobPostById
{
    public class GetJobPostByIdQuery : IRequest<JobPostDto>
    {
        public Guid Id { get; set; }
    }
}
