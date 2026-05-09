using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Feature.Advertisement.Command;
using JobFinder.Application.Repository;
using JobFinder.Application.Services;
using JobFinder.Application.Services.interfaces;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Handlers
{
    public class UpdateAdvertisementHandler : IRequestHandler<UpdateAdvertisementCommand>
    {
        private readonly IAdvertisementService _service;

        public UpdateAdvertisementHandler(IAdvertisementService service)
        {
            _service = service;
        }

        public async Task Handle(UpdateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            var advertisement = new Domain.Common.Entities.Advertisement
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                //CompanyName = request.CompanyName,
                //PostedDate = request.PostedDate
            };
            await _service.UpdateAdvertisementAsync(advertisement);
        }
    }
}
