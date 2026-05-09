using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Queries
{
    public class GetAdvertisementByIdQuery : IRequest<JobFinder.Domain.Common.Entities.Advertisement>
    {
        public Guid Id { get; set; }
    }
}
