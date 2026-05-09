using AutoMapper;
using FluentResults;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Customers.Queries;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Domain.Common.Models;
using MediaBrowser.Model.Querying;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace JobFinder.Application.Feature.Customers.Handlers.GetCustomersHandler
{
    public class GetCustomersHandler : IRequestHandler<GetCustomersQuery, Result<PagedResult<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCustomersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<PagedResult<CustomerDto>>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            var query = _unitOfWork.CustomerRepository.GetQueryable().Where(c => c.IsActive == true).ToList();
            var totalCount = await _unitOfWork.CustomerRepository.GetQueryable().CountAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                var searchTerm = request.SearchTerm.ToLower();
                query = query.Where(c => c.FirstName.ToLower().Contains(searchTerm) || c.LastName.ToLower().Contains(searchTerm)).ToList();
            }

            if (!string.IsNullOrEmpty(request.CustomerType))
                query = query.Where(c => c.CustomerType == request.CustomerType).ToList();


            var customers = query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var customerDtos = _mapper.Map<IEnumerable<CustomerDto>>(customers);

            var result = new PagedResult<CustomerDto>
            {
                Items = customerDtos,
                TotalItems = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };

            return Result.Ok(result);
        }
    }

}
