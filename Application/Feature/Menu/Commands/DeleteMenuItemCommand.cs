using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Menu.Commands
{
    //public class DeleteMenuItemCommand : IRequest<Result<bool>>
    //{
    //    public int Id { get; set; }
    //}
    public record DeleteMenuItemCommand(int Id) : IRequest<Result<bool>>;
}
