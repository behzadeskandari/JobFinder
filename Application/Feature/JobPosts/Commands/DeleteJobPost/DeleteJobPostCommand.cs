using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.JobPosts.Commands.DeleteJobPost
{
    public class DeleteJobPostCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
