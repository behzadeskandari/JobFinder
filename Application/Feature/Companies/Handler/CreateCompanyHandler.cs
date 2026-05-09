using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Companies.Command.CreateCompanyCommand;
using JobFinder.Application.Repository;
using JobFinder.Application.Services;
using JobFinder.Domain.Common.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace JobFinder.Application.Feature.Companies.Handler
{
    public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, Company>
    {
        private readonly IUnitOfWork _repository;
        private readonly ICurrentUserService _currentUserService;
        public CreateCompanyHandler(IUnitOfWork repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService; 
        }
        public async Task<Company> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {

            //var currentUser = _currentUserService.UserId;
            //var user = await _repository.UsersRepository.FindAsync(x => x.Id == currentUser);
            //var city = await _repository.CitiesRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == request.CityId);
            //if (user != null && user.Count() > 0)
            //{
            //    var company = new Company
            //    {
            //        Name = request.Name,
            //        Logo = request.Logo,
            //        Description = request.Description,
            //        Industry = request.Industry,
            //        Location = request.Location,
            //        Website = request.Website,
            //        Size = request.Size,
            //        CityId = request.CityId,  
            //        User = user.FirstOrDefault(),
            //        UserId = currentUser,
            //        City = city,
            //        Advertisements = new List<JobSeeker.Domain.Common.Entities.Advertisement>(),
            //        IndustryId = request.IndustryId,
            //        Jobs = new List<JobSeeker.Domain.Common.Entities.Job>(),
            //        LogoUrl = request.LogoUrl,
            //        Rating  = request.Rating,
            //        IsVerified = request.IsVerified,
            //        FoundedDate = request.FoundedDate,
            //        IsActive = request.IsActive,
            //        ContactEmail = request.ContactEmail,
            //        ContactPhone = request.ContactPhone,
            //        Benefits = request.Benefits.Select(b => new CompanyBenefit
            //        {
            //            Name = b.Name,
            //            Description = b.Description,
            //            Company = company,
            //            CompanyId == 
            //        }).ToList()
            //    };
            //    var t = await _repository.companyRepository.AddAsync(company);
            //    await _repository.CommitAsync(cancellationToken);
            //    return t;
            //}
            //return new Company();

            var currentUser = _currentUserService.UserId;
            var user = await _repository.UsersRepository.FindAsync(x => x.Id == currentUser);
            var city = await _repository.CitiesRepository.GetQueryable().FirstOrDefaultAsync(x => x.Id == request.CityId);

            if (user == null || !user.Any())
            {
                throw new Exception("User Not Signed In found.");
            }
            if (city == null)
            {
                throw new Exception($"City with ID {request.CityId} not found.");
            }

            var company = new Company
            {
                Name = request.Name,
                Logo = request.Logo,
                Description = request.Description,
                Industry = request.Industry,
                Location = request.Location,
                Website = request.Website,
                Size = request.Size,
                CityId = request.CityId,
                //User = user.FirstOrDefault(),
                UserId = currentUser,
                //City = city,
                Advertisements = new List<JobFinder.Domain.Common.Entities.Advertisement>(),
                JobCategoryId = request.IndustryId,
                Jobs = new List<JobFinder.Domain.Common.Entities.Job>(),
                LogoUrl = request.LogoUrl,
                Rating = request.Rating,
                IsVerified = request.IsVerified,
                FoundedDate = request.FoundedDate,
                IsActive = false, // Ensure IsActive is set
                ContactEmail = request.ContactEmail,
                ContactPhone = request.ContactPhone,
                Benefits = new List<CompanyBenefit>() // Initialize empty to avoid null issues
            };

            // Add the company to the repository first
            var addedCompany = await _repository.companyRepository.AddAsync(company);
            var id = await _repository.CommitAsync(cancellationToken); // Save to generate Company.Id
            //addedCompany.Id = id;
            // Now create and add CompanyBenefit entities
            if (request.Benefits?.Any() == true)
            {
                var benefits = request.Benefits.Select(b => new CompanyBenefit
                {
                    Name = b.Name,
                    Description = b.Description,
                    CompanyId = addedCompany.Id, // Use the generated Company Id
                                                 // Do not set Company navigation property to avoid circular reference
                    IsActive = true // Align with SaveChangesAsync logic
                }).ToList();

                foreach (var benefit in benefits)
                {
                    await _repository.CompanyBenefitsReposity.AddAsync(benefit);
                }
                await _repository.CommitAsync(cancellationToken); // Save benefits
            }

            return addedCompany;
        }
    }
}
