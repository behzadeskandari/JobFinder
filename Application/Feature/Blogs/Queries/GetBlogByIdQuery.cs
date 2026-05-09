using FluentResults;
using JobFinder.Contracts.Dtos.Blogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Blogs.Queries
{
    public class GetBlogByIdQuery : MediatR.IRequest<Result<BlogDto>>
    {
        public int Id { get; set; }
    }
}
