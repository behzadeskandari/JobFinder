using FluentResults;
using JobFinder.Contracts.Dtos.DropDown;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Feature.DropDowns.Province.Queries
{
    public class GetProvinceById : IRequest<Result<List<JobFinder.Domain.Common.Entities.Province>>>
    {
        public int Id { get; set; }

    }
}
