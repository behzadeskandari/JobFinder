using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.Product.Queries.GetProductByIdQuery
{
    public record GetProductByIdQuery(Guid Id) : IRequest<Result<JobFinder.Domain.Common.Entities.Product>>;

}
