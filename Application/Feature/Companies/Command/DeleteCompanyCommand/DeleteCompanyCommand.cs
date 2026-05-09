using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Command.DeleteCompanyCommand
{
    public class DeleteCompanyCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
