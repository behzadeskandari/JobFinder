using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentResults;
using JobFinder.Contracts.Enums;
using MediatR;

namespace JobFinder.Application.Feature.JobOffer.Command
{
    public record UpdateJobOfferCommand(int Id, string Details, decimal? SalaryOffered, DateTime? ExpiresAt, JobOfferStatus Status, string Title, bool? IsActive) : IRequest<Result<bool>>;

}
