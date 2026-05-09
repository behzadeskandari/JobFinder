using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobOffer.Queries;
using MediatR;

namespace JobFinder.Application.Feature.JobOffer.Handler
{
    //public class GetAllJobOffersHandler : IRequestHandler<GetAllJobOffersQuery, Result<List<JobFinder.Domain.Common.Entities.JobOffer>>>
    //{
    //    private readonly IUnitOfWork _context;

    //    public GetAllJobOffersHandler(IUnitOfWork context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<Result<List<JobFinder.Domain.Common.Entities.JobOffer>>> Handle(GetAllJobOffersQuery request, CancellationToken cancellationToken)
    //    {
    //        var jobOffers = await _context.JobOffersRepository.GetAllAsync(cancellationToken);
    //        return Result.Ok(jobOffers.ToList());
    //    }
    //}
}
