using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using MediatR;

namespace JobFinder.Application.Feature.JobOffer.Command
{
    public record DeleteJobOfferCommand(int Id) : IRequest<Result<bool>>;

}
