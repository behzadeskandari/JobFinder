using JobFinder.Contracts.Dtos.JobPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Commands.CreateJobPost
{
    public class CreateJobPostCommand : IRequest<Guid>
    {
        public CreateJobPostDto JobPost { get; set; }
    }

}
