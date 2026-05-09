using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Command.DeleteCompanyCommand;
using JobFinder.Application.Repository;
using MediatR;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, bool>
    {
        private readonly IUnitOfWork _repository;

        public DeleteCompanyHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _repository.companyRepository.GetByIdAsync(request.Id);

            if (company == null)
                throw new NotFoundException("شرکت پیدا نشد");

            await _repository.companyRepository.DeleteAsync(request.Id);
            await _repository.CommitAsync(cancellationToken);
            return true;
        }
    }
}
