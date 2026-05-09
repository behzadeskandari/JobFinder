using JobFinder.Contracts.Dtos.JobPost;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Commands.UpdateJobPost
{
    public class UpdateJobPostCommand : IRequest
    {
        public Guid Id { get; set; }
        public UpdateJobPostDto JobPost { get; set; }
    }

}
