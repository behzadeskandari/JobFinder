using FluentResults;
using JobFinder.Contracts.Dtos.Blogs;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Blogs.Queries
{
    public class GetBlogsQuery : MediatR.IRequest<Result<PagedResult<BlogDto>>>
    {
        public string? SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
