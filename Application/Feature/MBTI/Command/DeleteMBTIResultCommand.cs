using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.MBTI.Command
{
    public class DeleteMBTIResultCommand : IRequest<Result>
    {
        public int Id { get; set; }
    }
}
