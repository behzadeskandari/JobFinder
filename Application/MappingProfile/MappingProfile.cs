using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Feature.Menu.Commands;
using JobFinder.Contracts.Dtos.Advertisement;
using JobFinder.Contracts.Dtos.Blogs;
using JobFinder.Contracts.Dtos.Category;
using JobFinder.Contracts.Dtos.Company;
using JobFinder.Contracts.Dtos.CompanyFollows;
using JobFinder.Contracts.Dtos.Customer;
using JobFinder.Contracts.Dtos.DropDown;
using JobFinder.Contracts.Dtos.Feature;
using JobFinder.Contracts.Dtos.Job;
using JobFinder.Contracts.Dtos.JobPost;
using JobFinder.Contracts.Dtos.JobRequest;
using JobFinder.Contracts.Dtos.Menu;
using JobFinder.Contracts.Dtos.Order;
using JobFinder.Contracts.Dtos.Payment;
using JobFinder.Contracts.Dtos.PersonalityTest;
using JobFinder.Contracts.Dtos.Pricing;
using JobFinder.Contracts.Dtos.Product;
using JobFinder.Contracts.Dtos.PsychologyTest;
using JobFinder.Contracts.Dtos.PsychologyTestResult;
using JobFinder.Contracts.Dtos.Resume;
using JobFinder.Contracts.Dtos.SavedJobs;
using JobFinder.Domain.Common.Entities;

namespace Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<PricingCategory, PricingCategoryDto>();
            CreateMap<PricingPlan, PricingPlanDto>();
            CreateMap<PricingFeature, PricingFeatureDto>();
            CreateMap<JobFinder.Domain.Common.Entities.Feature, FeatureDto>();
            CreateMap<MenuItem, MenuItemDto>();

            // Job Post mappings
            CreateMap<JobPost, JobPostDto>()
                .ForMember(d => d.StaffEmail, opt => opt.MapFrom(s => s.Staff.Email));

            // Job Request mappings
            CreateMap<JobRequest, JobRequestDto>()
                .ForMember(d => d.UserEmail, opt => opt.MapFrom(s => s.User.Email));
            //.ForMember(d => d.JobPostTitle, opt => opt.MapFrom(s => s.JobPost.Title));

            // Job Offer mappings
            //CreateMap<JobOffer, JobOfferDto>()
            //    .ForMember(d => d.UserEmail, opt => opt.MapFrom(s => s.User.Email))
            //    .ForMember(d => d.JobPostTitle, opt => opt.MapFrom(s => s.Title));

            // Advertisement mappings
            CreateMap<Advertisement, AdvertisementDto>()
                .ForMember(d => d.StaffEmail, opt => opt.MapFrom(s => s.Staff.Email))
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name));

            // Category mappings
            CreateMap<Category, CategoryDto>();

            // Payment mappings
            CreateMap<Payment, PaymentDto>()
                //.ForMember(d => d.StaffEmail, opt => opt.MapFrom(s => s.Staff.Email))
                .ForMember(d => d.AdvertisementTitle, opt => opt.MapFrom(s => s.Advertisement.Title));

            CreateMap<MenuItem, MenuItemDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
            CreateMap<CreateMenuItemCommand, MenuItem>();
            CreateMap<UpdateMenuItemCommand, MenuItem>();

            CreateMap<Job, JobGetDto>().ReverseMap();
            CreateMap<JobApplication, JobApplicationDto>().ReverseMap();

            CreateMap<Job, JobGetDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Cities.Label))
            .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategories.Name))
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.ToString()));

            CreateMap<JobApplication, JobApplicationDto>();
            CreateMap<JobCategory, JobCategoryDto>();
            CreateMap<SavedJob, SavedJobDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));


            CreateMap<Job, JobGetDto>()
         .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
         .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Cities.Label))
         .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategories.Name))
         .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.ToString()));

            CreateMap<JobApplication, JobApplicationDto>();
            CreateMap<JobCategory, JobCategoryDto>();
            CreateMap<SavedJob, SavedJobDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));

            //CreateMap<Company, CompanyDto>()
            //    .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Label));

            CreateMap<CompanyFollow, CompanyFollowDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));

            CreateMap<Job, JobGetDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
            .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Cities.Label))
            .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategories.Name))
            .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.ToString()));

            CreateMap<JobApplication, JobApplicationDto>();
            CreateMap<JobCategory, JobCategoryDto>();
            CreateMap<SavedJob, SavedJobDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));

            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry));
            //  .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Label));

            CreateMap<CompanyFollow, CompanyFollowDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));


            CreateMap<Job, JobGetDto>()
        .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name))
        .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Cities.Label))
        .ForMember(dest => dest.JobCategoryName, opt => opt.MapFrom(src => src.JobCategories.Name))
        .ForMember(dest => dest.JobType, opt => opt.MapFrom(src => src.JobType.ToString()));
            CreateMap<JobApplication, JobApplicationDto>();
            CreateMap<JobCategory, JobCategoryDto>();
            CreateMap<SavedJob, SavedJobDto>()
                .ForMember(dest => dest.JobTitle, opt => opt.MapFrom(src => src.Job.Title));
            CreateMap<Company, CompanyDto>()
                .ForMember(dest => dest.IndustryName, opt => opt.MapFrom(src => src.Industry));
            /// .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.City.Label));
            CreateMap<CompanyFollow, CompanyFollowDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
            CreateMap<Resume, ResumeDto>();
            //CreateMap<CandidateJobPreferences, CandidateJobPreferenceDto>()
            //    .ForMember(dest => dest.JobCategory.Label, opt => opt.MapFrom(src => src.JobCategory.Name != null ? src.JobCategory.Name : ""))
            //    .ForMember(dest => dest.City.Label, opt => opt.MapFrom(src => src.City.Label != null ? src.City.Label :""));
            //CreateMap<CandidateJobPreferences, CandidateJobPreferenceDto>()
            //    .ForMember(dest => dest.JobCategory.Label, opt => opt.MapFrom(src => src.JobCategory.Name != null ? src.JobCategory.Name : ""))
            //    .ForMember(dest => dest.City.Label, opt => opt.MapFrom(src => src.City.Label != null ? src.City.Label : ""));
            CreateMap<PsychologyTest, PsychologyTestDto>();
            CreateMap<PsychologyTestResult, PsychologyTestResultDto>()
                .ForMember(dest => dest.PsychologyTestTitle, opt => opt.MapFrom(src => src.PsychologyTest.Name));
            CreateMap<PersonalityTrait, PersonalityTestDto>();
            //CreateMap<PersonalityTestResult, PersonalityTestResultDto>()
            //    .ForMember(dest => dest.PersonalityTestTitle, opt => opt.MapFrom(src => src.Candidates.Title));
            CreateMap<Order, OrderGetDto>()
           .ForMember(dest => dest.PricingPlanName, opt => opt.MapFrom(src => src.PricingPlan.Name));
            CreateMap<Province, ProvinceDto>();
            CreateMap<City, CityDto>()
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label));
            CreateMap<TechnicalOption, TechnicalOptionDto>();
            CreateMap<PricingPlan, PricingPlanDto>()
                .ForMember(dest => dest.PricingCategoryName, opt => opt.MapFrom(src => src.PricingCategory.Name));
            CreateMap<PricingCategory, PricingCategoryDto>();
            CreateMap<JobFinder.Domain.Common.Entities.Feature, FeatureDto>();
            CreateMap<Blog, BlogDto>();

            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Resume, ResumeDto>().ReverseMap();


            CreateMap<Product, ProductDto>().ReverseMap()
                .ForMember(dest => dest.DateCreated, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.DateModified, opt => opt.Ignore())
                .ForMember(dest => dest.SalesOrderItems, opt => opt.Ignore())
                .ForMember(dest => dest.ProductInventories, opt => opt.Ignore())
                .ForMember(dest => dest.ProductInventorySnapshots, opt => opt.Ignore());

            //CreateMap<FaqCategory, FaqDto>();

        }
    }

}
