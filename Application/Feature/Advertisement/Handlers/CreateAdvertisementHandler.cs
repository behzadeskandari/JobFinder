using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Exceptions;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Feature.Advertisement.Command;
using JobFinder.Application.Repository;
using JobFinder.Domain.Common.Entities;
using MediatR;

namespace JobFinder.Application.Feature.Advertisement.Handlers
{
    public class CreateAdvertisementHandler : IRequestHandler<CreateAdvertisementCommand, Guid>
    {
        private readonly IUnitOfWork _repository;
        private readonly ICurrentUserService _currentUserService;

        public CreateAdvertisementHandler(IUnitOfWork repository, ICurrentUserService currentUserService)
        {
            _repository = repository;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateAdvertisementCommand request, CancellationToken cancellationToken)
        {
            User? user = null;
            if (string.IsNullOrEmpty(_currentUserService.UserEmail) || string.IsNullOrEmpty(_currentUserService.UserId))
            {
                throw new NotFoundException("شما باید وارد سیستم شوید و ثبت نام کنید و برای تبلیغات در سایت ما یک شرکت ایجاد کنید");
            }
            else
            {
                var userId = _currentUserService.UserId;
                user = await _repository.UsersRepository.GetByIdAsync(userId);
            }

            var category = await _repository.CategoryRepository.GetByIdAsync(request.CategoryId);
            if (category == null) {
                throw new NotFoundException("دسته بندی صحیح نیست");
            }

            var comapny = await _repository.companyRepository.GetByIdAsync(request.CompanyId);
            if (comapny == null)
            {
                throw new NotFoundException("دسته بندی صحیح نیست");
            }
            if (comapny.UserId != _currentUserService.UserId)
            {
                throw new NotFoundException("شرکت شما نیست");
            }


            var advertisement = new Domain.Common.Entities.Advertisement
            {
                Title = request.Title,
                Description = request.Description,
                ExpiresAt = request.ExpiresAt,
                ImageUrl = request.ImageUrl,
                IsActive = request.IsActive,
                IsApproved = request.IsApproved,
                Category = category,
                CategoryId = category.Id,
                IsPaid = request.IsPaid,
                JobADVCreatedAt = request.JobADVCreatedAt,
                Company = comapny,
                CompanyId = comapny.Id,
                Staff = user,
                StaffId = user.Id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                PaymentId = Guid.Empty,
                //CompanyName = request.CompanyName,
                //PostedDate = request.PostedDate
            };

            await _repository.AdvertisementRepository.AddAdvertisementAsync(advertisement);

            return advertisement.Id;
        }
    }
}
