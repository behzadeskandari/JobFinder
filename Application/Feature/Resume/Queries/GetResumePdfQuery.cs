using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Resume.Queries
{
    public class GetResumePdfQuery : IRequest<Result<byte[]>>
    {
        public Guid Id { get; set; }
    }
}
