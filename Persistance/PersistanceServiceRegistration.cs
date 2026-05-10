using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Behaviors;
using Domain.WriteRepository;
using JobFinder.Application.Common.Interfaces;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Repositories.Invoice;
using JobFinder.Application.Repository;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Application.Services.interfaces;
using JobFinder.Contracts.Validations;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JobFinder.Persistance.Repositories;
using Persistance.DatabaseContext.UnitOfWork;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Persistance.Repositories.GenericRepository;
using JobFinder.Application.Services;
using JobFinder.Infrastructure.Persistence.Repositories;
using Persistance.Services;
using Persistance.Interfaces;

namespace Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<JobSeekerDataContext>(options =>
            //{
            //    options.UseSqlServer(configuration.GetConnectionString("HrDatabaseConnectionString"));
            //});

            //services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddValidatorsFromAssemblyContaining<MBTIQuestionValidator>();
            services.AddValidatorsFromAssemblyContaining<MBTIResultValidator>();

            // Register FluentValidation with MediatR
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));

            services.AddScoped<ILogsRepository,LogsRepository>();

            //services.AddScoped(typeof(IReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Register repositories.  Use AddScoped for web requests, AddSingleton for app lifetime.

            //services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IInventoryRepository, InventoryRepository>();
            services.AddScoped<IJobCategoryRepository, JobCategoryRepository>();
            services.AddScoped<IMBTIQuestionRepository, MBTIQuestionRepository>();
            services.AddScoped<IMBTIResultRepository, MBTIResultRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<ITechnicalOptionsRepository, TechnicalOptionsRepository>();
            services.AddScoped<IMBTICalculationService, MBTICalculationService>();
            services.AddScoped<ICandidateRepository, CandidateRepository>();
            services.AddScoped<IResumeRepository, ResumeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICompanyBenefitsReposity, CompanyBenefitsReposity>();
            services.AddScoped<ITermsSectionsRepository, TermsSectionsRepository>();
            services.AddScoped<ITermsOfServicesRepository, TermsOfServicesRepository>();
            services.AddScoped<ILanguagesRepository, LanguagesRepository>();
            services.AddScoped<IWorkExperiencesRepository, WorkExperiencesRepository>();
            services.AddScoped<IEducationsRepository, EducationsRepository>();
            services.AddScoped<IPricingCategoriesRepository, PricingCategoriesRepository>();
            services.AddScoped<ISkillsRepository, SkillsRepository>();
            services.AddScoped<IPricingFeaturesRepository, PricingFeaturesRepository>();
            services.AddScoped<IPricingPlansRepository, PricingPlansRepository>();
            services.AddScoped<IFaqCategoriesRepository, FaqCategoriesRepository>();
            services.AddScoped<IFaqQuestionsRepository, FaqQuestionsRepository>();
            services.AddScoped<ICustomerAddressesRepository, CustomerAddressesRepository>();
            services.AddScoped<IFeaturesRepository, FeaturesRepository>();
            services.AddScoped<IJobPostsRepository, JobPostsRepository>();
            services.AddScoped<IJobRequestsRepository, JobRequestsRepository>();
            services.AddScoped<IJobsRepository, JobsRepository>();
            services.AddScoped<ICompanyJobPreferencesRepository, CompanyJobPreferencesRepository>();
            services.AddScoped<ICandidateJobPreferences, CandidateJobPreferencesRepository>();
            services.AddScoped<IJobTestAssignment, JobTestAssignmentRepository>();
            services.AddScoped<IPersonalityTestItem, PersonalityTestItemRepository>();
            services.AddScoped<IPersonalityTestResponse, PersonalityTestResponseRepository>();
            services.AddScoped<IPersonalityTestResult, PersonalityTestResultRepository>();
            services.AddScoped<IPersonalityTrait, PersonalityTraitRepository>();
            services.AddScoped<IPsychologyTest, PsychologyTestRepository>();
            services.AddScoped<IPsychologyTestQuestion, PsychologyTestQuestionRepository>();
            services.AddScoped<IPsychologyTestResponse, PsychologyTestResponseRepository>();
            services.AddScoped<IPsychologyTestResult, PsychologyTestResultRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUserSettingRepository, UserSettingRepository>();
            services.AddScoped<ISavedJobRepository, SavedJobRepository>();
            services.AddScoped<ICompanyFollowRepository, CompanyFollowRepository>();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IProductInventories, ProductInventoriesRepository>();
            services.AddScoped<IProductInventorySnapshots, ProductInventorySnapshotsRepository>();
            services.AddScoped<ISalesOrderItems, SalesOrderItemsRepository>();
            services.AddScoped<ISalesOrders, SalesOrdersRepository>();
            services.AddScoped<IInterviewDetails, InterviewDetailsRepository>();
            services.AddScoped<IJobApplication, JobApplicationRepository>();
            services.AddScoped<IOfferDetails, OfferDetailsRepository>();
            services.AddScoped<IRejectionDetails, RejectionDetailsRepository>();
            services.AddScoped<ISubmissionDetails, SubmissionDetailsRepository>();
            services.AddScoped<IBlogRepository, BlogRepository>();
            //services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<WriteDbContext>());
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<ITermsService, TermsService>();
            services.AddScoped<IPdfService, PdfService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
            services.AddScoped<IExceptionLogger, ExceptionLogger>();
            services.AddTransient<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IMenuRepository, MenuItemRepository>();
            services.AddScoped<ICandidateAnalyticsService, CandidateAnalyticsService>();
            services.AddScoped<IMatchingService, MatchingService>();
            services.AddScoped<IJobPostAnalyticsService, JobPostAnalyticsService>();
            services.AddScoped<IEmployerAnalyticsService, EmployerAnalyticsService>();
            services.AddScoped<IAdminAnalyticsService, AdminAnalyticsService>();
            services.AddScoped<IPsychologyTestService, PsychologyTestService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserSettingService, UserSettingService>();
            //services.AddSingleton<GenericDatabaseSyncService>();
            return services;
        }
    }

}
