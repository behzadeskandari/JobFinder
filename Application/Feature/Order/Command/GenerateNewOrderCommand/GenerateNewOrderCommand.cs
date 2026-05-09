using FluentResults;
using JobFinder.Contracts.Dtos.Invoice;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.Order.Command.GenerateNewOrderCommand
{
    public class GenerateNewOrderCommand : IRequest<Result>
    {
        public InvoiceDto Invoice { get; set; }
    }
}
