using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Advertisement.Queries;
using JobFinder.Application.Repository;
using JobFinder.Application.Services.interfaces;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Handlers
{
    public class GetAdvertisementsHandler : IRequestHandler<GetAdvertisementsQuery, IEnumerable<JobFinder.Domain.Common.Entities.Advertisement>>
    {
        private readonly IAdvertisementService _repository;

        public GetAdvertisementsHandler(IAdvertisementService repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<JobFinder.Domain.Common.Entities.Advertisement>> Handle(GetAdvertisementsQuery request, CancellationToken cancellationToken)
        {
            var record = await _repository.GetAdvertisementsAsync();
            return record;
        }
    }
}
