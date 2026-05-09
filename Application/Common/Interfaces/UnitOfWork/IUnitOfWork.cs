using JobFinder.Application.Repositories.Invoice;
using JobFinder.Application.Repository;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Application.Common.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IMBTIQuestionRepository MBTIQuestionRepository { get; }
        IMBTIResultRepository MBTIResultRepository { get; }
        IMBTIResultAnswersRepository MBTIResultAnswersRepository { get; }
        ITechnicalOptionsRepository TechnicalOptionsRepository { get; }
        IJobCategoryRepository JobCategoryRepository { get; }
        ICityRepository CitiesRepository { get; }
        IProvinceRepository ProvincesRepository { get; }
        ICustomerRepository CustomerRepository { get; }
        IInventoryRepository InventoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IProductRepository ProductRepository { get; }
        ICandidateRepository CandidateRepository { get; }
        IResumeRepository ResumeRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        ILogsRepository LogsRepository { get;  }
        IAdvertisementRepository AdvertisementRepository { get; }
        ICompanyBenefitsReposity CompanyBenefitsReposity { get; }
        ITermsSectionsRepository TermsSectionsRepository { get; }
        ITermsOfServicesRepository TermsOfServicesRepository { get; }
        ILanguagesRepository LanguagesRepository { get; }
        IWorkExperiencesRepository WorkExperiencesRepository { get; }
        IEducationsRepository EducationsRepository { get; }
        ISkillsRepository SkillsRepository { get; }
        IPricingFeaturesRepository PricingFeaturesRepository { get; }
        IPricingCategoriesRepository PricingCategoriesRepository { get; }
        IPricingPlansRepository PricingPlansRepository { get; }
        IFaqCategoriesRepository FaqCategoriesRepository { get; }
        IFaqQuestionsRepository FaqQuestionsRepository { get; }
        ICustomerAddressesRepository CustomerAddressesRepository { get; }
        IFeaturesRepository FeaturesRepository { get; }
        IJobPostsRepository JobPostsRepository { get; }
        //IJobOffersRepository JobOffersRepository { get; }
        IJobRequestsRepository JobRequestsRepository { get; }
        IJobsRepository JobsRepository { get; }
        ICompanyRepository companyRepository { get; }
        ICompanyJobPreferencesRepository companyJobPreferences { get; }
        ICandidateJobPreferences candidateJobPreferences { get; }
        IJobTestAssignment jobTestAssignment { get; }

        IPersonalityTestItem personalityTestItem { get; }
        IPersonalityTestResponse personalityTestResponse { get; }
        IPersonalityTestResult personalityTestResult { get; }
        IPersonalityTrait personalityTrait { get; }
        IPsychologyTest psychologyTest { get; }
        IPsychologyTestQuestion psychologyTestQuestion { get; }
        IPsychologyTestResponse psychologyTestResponse { get; }
        IPsychologyTestResponseAnswer psychologyTestResponseAnswer { get; }
        IPsychologyTestResultAnswer psychologyTestResultAnswer { get; }
        IPsychologyTestResult psychologyTestResult { get; }
        IUsersRepository UsersRepository { get; }
        IPaymentsRepository PaymentsRepository { get; }
        IProductInventories ProductInventories { get; }
        IProductInventorySnapshots ProductInventorySnapshots { get; }
        ISalesOrderItems SalesOrderItems { get; }
        ISalesOrders SalesOrders { get; }
        IInterviewDetails InterviewDetails { get; }
        IJobApplication JobApplication { get; }
        IOfferDetails OfferDetails { get; }
        IRejectionDetails RejectionDetails { get; }
        ISubmissionDetails SubmissionDetails { get; }
        IUserSettingRepository UserSettingRepository { get; }
        ISavedJobRepository SavedJob { get; }
        ICompanyFollowRepository CompanyFollowRepository { get; }
        IBlogRepository BlogRepository { get; }
        IAsnwerRepository AnswerRepository { get; }

        Task BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);

        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
        Task<int> CommitAsync(CancellationToken cancellationToken =default);
    }
}
