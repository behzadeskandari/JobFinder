using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Blogs.Queries;
using JobFinder.Contracts.Dtos.Blogs;
using JobFinder.Domain.Common.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Blogs.Handlers
{
    public class GetBlogByIdQueryHandler : IRequestHandler<GetBlogByIdQuery, Result<BlogDto>>
{
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBlogByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<BlogDto>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var blog = await _unitOfWork.BlogRepository
                .GetByIdAsync(request.Id);

            if (blog == null || blog.IsActive != true)
                throw new NotFoundException("وبلاگ یافت نشد یا غیرفعال است");

            var blogDto = _mapper.Map<BlogDto>(blog);
            return Result.Ok(blogDto);
        }
    }
}
