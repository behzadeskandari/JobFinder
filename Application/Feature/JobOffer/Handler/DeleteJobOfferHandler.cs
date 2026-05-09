using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.JobOffer.Command;
using MediatR;

namespace JobFinder.Application.Feature.JobOffer.Handler
{

    //public class DeleteJobOfferHandler : IRequestHandler<DeleteJobOfferCommand, Result<bool>>
    //{
    //    private readonly IUnitOfWork _context;

    //    public DeleteJobOfferHandler(IUnitOfWork context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<Result<bool>> Handle(DeleteJobOfferCommand request, CancellationToken cancellationToken)
    //    {
    //        var jobOffer = await _context.JobOffersRepository.GetByIdAsync(request.Id);
    //        if (jobOffer == null)
    //        {
    //            return Result.Fail($"JobOffer with Id {request.Id} not found.");
    //        }

    //        await _context.JobOffersRepository.DeleteAsync(jobOffer);
    //        await _context.CommitAsync(cancellationToken);
    //        return Result.Ok(true);
    //    }
    //}

}
