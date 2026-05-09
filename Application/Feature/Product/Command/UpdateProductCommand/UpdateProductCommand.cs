using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.Product.Command.UpdateProductCommand
{
    public record UpdateProductCommand(int Id, string Name, string Description, decimal Price, bool IsTaxable, bool IsArchived, DateTime UpdatedOn, bool? IsActive) : IRequest<Result<bool>>;

}
