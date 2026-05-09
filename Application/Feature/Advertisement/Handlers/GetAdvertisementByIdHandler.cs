using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Advertisement.Queries;
using JobFinder.Application.Repository;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Handlers
{

    public class GetAdvertisementByIdHandler : IRequestHandler<GetAdvertisementByIdQuery, JobFinder.Domain.Common.Entities.Advertisement>
    {
        private readonly IAdvertisementRepository _repository;

        public GetAdvertisementByIdHandler(IAdvertisementRepository repository)
        {
            _repository = repository;
        }

        public async Task<JobFinder.Domain.Common.Entities.Advertisement> Handle(GetAdvertisementByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAdvertisementByIdAsync(request.Id);
        }
    }
}
