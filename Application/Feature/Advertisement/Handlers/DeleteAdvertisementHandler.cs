using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Advertisement.Command;
using JobFinder.Application.Repository;
using JobFinder.Application.Services.interfaces;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Handlers
{
    public class DeleteAdvertisementHandler : IRequestHandler<DeleteAdvertisementCommand>
    {
        private readonly IAdvertisementService _repository;

        public DeleteAdvertisementHandler(IAdvertisementService repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAdvertisementAsync(request.Id);
        }
    }
}
