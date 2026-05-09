using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Command.MarkOrderCompleteCommand
{
    public class MarkOrderCompleteCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
