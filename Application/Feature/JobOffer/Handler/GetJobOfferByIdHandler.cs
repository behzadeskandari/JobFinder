using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobOffer.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.JobOffer.Handler
{

    //public class GetJobOfferByIdHandler : IRequestHandler<GetJobOfferByIdQuery, Result<JobFinder.Domain.Common.Entities.JobOffer>>
    //{
    //    private readonly IUnitOfWork _context;

    //    public GetJobOfferByIdHandler(IUnitOfWork context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<Result<JobFinder.Domain.Common.Entities.JobOffer>> Handle(GetJobOfferByIdQuery request, CancellationToken cancellationToken)
    //    {
    //        var jobOffer = await _context.JobOffersRepository.GetQueryable().FirstOrDefaultAsync(jo => jo.Id == request.Id, cancellationToken);
    //        if (jobOffer == null)
    //        {
    //            return Result.Fail($"JobOffer with Id {request.Id} not found.");
    //        }
    //        return Result.Ok(jobOffer);
    //    }
    //}
}
