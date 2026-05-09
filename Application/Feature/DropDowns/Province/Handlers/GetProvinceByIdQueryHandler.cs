using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.DropDowns.Province.Queries;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Province.Handlers
{
    internal class GetProvinceByIdQueryHandler : IRequestHandler<GetProvinceById, Result<List<JobFinder.Domain.Common.Entities.Province>>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProvinceByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Result<List<JobFinder.Domain.Common.Entities.Province>>> Handle(GetProvinceById request, CancellationToken cancellationToken)
        {
            var record =  _unitOfWork.ProvincesRepository.GetQueryable().Where(x => x.Id == request.Id).ToList();
            var tr =  Result.Ok(record);
            return tr.Value;
            //throw new NotImplementedException();
        }
    }
}
