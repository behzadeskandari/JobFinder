using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Application.Common.Interfaces.UnitOfWork;
using JobFinder.Application.Repositories.Invoice;
using JobFinder.Application.Repository;
using JobFinder.Application.Repository.Invoice;
using JobFinder.Infrastructure.Persistence.Repositories;
using JobFinder.Persistance.Repositories;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Persistance.DatabaseContext.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WriteDbContext.WriteDbContext _writeContext;
        private readonly ReadDbContext.ReadDbContext _readContext;
        private ILoggerFactory _loggerFactory;
        private IMapper _IMapper;
        private readonly IAccountService _accountService;
        //private readonly IConfiguration _configuration;
        //private Dictionary<Type, object> _writeRepositories;
        //private Dictionary<Type, object> _readRepositories;
        private IDbContextTransaction _transaction;
        public ICityRepository _cityRepository;
        public IMBTIQuestionRepository _mBTIQuestionRepository;
        public IMBTIResultRepository _mBTIResultRepository;
        public ITechnicalOptionsRepository _technicalOptionsRepository;
        public IJobCategoryRepository _jobCategoryRepository;
        public ILogsRepository _logsRepository;
        public IProvinceRepository _provincesRepository;
        public ICustomerRepository _customerRepository;
        public IInventoryRepository _inventoryRepository;
        public IOrderRepository _orderRepository;
        public IProductRepository _productRepository;
        public ICandidateRepository _candidateRepository;
        public IResumeRepository _resumeRepository;
        public IAdvertisementRepository _advertisementRepository;
        public ICategoryRepository _categoryRepository;
        public ICompanyRepository _companyRepository;
        public ICompanyBenefitsReposity _companyBenefitsReposity;
        public ITermsSectionsRepository _termsSectionsRepository;
        public ITermsOfServicesRepository _termsOfServicesRepository;
        public ILanguagesRepository _languagesRepository;
        public IWorkExperiencesRepository _workExperiencesRepository;
        public IEducationsRepository _educationsRepository;
        public ISkillsRepository _skillsRepository;
        public IPricingFeaturesRepository _pricingFeaturesRepository;
        public IPricingCategoriesRepository _pricingCategoriesRepository;
        public IPricingPlansRepository _pricingPlansRepository;
        public IFaqCategoriesRepository _faqCategoriesRepository;
        public IFaqQuestionsRepository _faqQuestionsRepository;
        public ICustomerAddressesRepository _customerAddressesRepository;
        public IFeaturesRepository _featuresRepository;
        public IJobPostsRepository _jobPostsRepository;
        //public IJobOffersRepository _jobOffersRepository;
        public IJobRequestsRepository _jobRequestsRepository;
        public IJobsRepository _jobsRepository;
        public ICandidateJobPreferences _candidateJobPreferences;
        public ICompanyJobPreferencesRepository _companyJobPreferences;
        public IJobTestAssignment _jobTestAssignment;
        public IPersonalityTestItem _personalityTestItem;
        public IPersonalityTestResponse _personalityTestResponse;
        public IPersonalityTestResult _personalityTestResult;
        public IPersonalityTrait _personalityTrait;
        public IPsychologyTest _psychologyTest;
        public IPsychologyTestQuestion _psychologyTestQuestion;
        public IPsychologyTestResponse _psychologyTestResponse;
        public IPsychologyTestResult _psychologyTestResult;
        public IUsersRepository _usersRepository;
        public IPaymentsRepository _paymentsRepository;
        public IProductInventories _productInventories;
        public IProductInventorySnapshots _productInventorySnapshots;
        public ISalesOrderItems _salesOrderItems;
        public ISalesOrders _salesOrders;
        public IInterviewDetails _interviewDetails;
        public IJobApplication _jobApplication;
        public IOfferDetails _offerDetails;
        public IRejectionDetails _rejectionDetails;
        public ISubmissionDetails _submissionDetails;
        public IUsersRepository _userRepository;
        public IUserSettingRepository _userSettingRepository;
        public ISavedJobRepository _savedJob;
        public ICompanyFollowRepository _companyFollowRepository;
        public IBlogRepository _blogRepository;
        public IAsnwerRepository _asnwerRepository;
        public IMBTIResultAnswersRepository _mbtiResultAnswersRepository;
        public IPsychologyTestResponseAnswer _psychologyTestResponseAnswer;
        public IPsychologyTestResultAnswer _psychologyTestResultAnswer;
        public UnitOfWork(IMapper IMapper,
            //LogsRepository logsRepository,
                          WriteDbContext.WriteDbContext writeContext,
                          ReadDbContext.ReadDbContext readContext,
                          ILoggerFactory loggerFactory,
                          IAccountService accountService)
        {
            _writeContext = writeContext ?? throw new ArgumentNullException(nameof(writeContext));
            _readContext = readContext ?? throw new ArgumentNullException(nameof(readContext));
            //_configuration = configuration;
            //_writeRepositories = new Dictionary<Type, object>();
            //_readRepositories = new Dictionary<Type, object>();
            _loggerFactory = loggerFactory;
            _IMapper = IMapper;
            _accountService = accountService;

        }

        public IMBTIQuestionRepository MBTIQuestionRepository => _mBTIQuestionRepository ??= new MBTIQuestionRepository(_writeContext, _readContext);

        public IMBTIResultRepository MBTIResultRepository => _mBTIResultRepository ??= new MBTIResultRepository(_writeContext, _readContext);//(IMBTIResultRepository)GetWriteRepository<MBTIResult>();
        public ITechnicalOptionsRepository TechnicalOptionsRepository => _technicalOptionsRepository ??= new TechnicalOptionsRepository(_writeContext, _readContext);
        public IJobCategoryRepository JobCategoryRepository => _jobCategoryRepository ??= new JobCategoryRepository(_writeContext, _readContext);
        public ICityRepository CitiesRepository => _cityRepository ??= new CityRepository(_writeContext, _readContext);



        public IProvinceRepository ProvincesRepository => _provincesRepository ??= new ProvinceRepository(_writeContext, _readContext);
        public ICustomerRepository CustomerRepository => _customerRepository ??= new CustomerRepository(_writeContext, _readContext);
        public IInventoryRepository InventoryRepository => _inventoryRepository ??= new InventoryRepository(_writeContext, _readContext, _loggerFactory.CreateLogger<InventoryRepository>());
        public IOrderRepository OrderRepository => _orderRepository ??= new OrderRepository(_writeContext, _readContext, _loggerFactory.CreateLogger<OrderRepository>(), _productRepository, _inventoryRepository);
        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_writeContext, _readContext);
        public ICandidateRepository CandidateRepository => _candidateRepository ??= new CandidateRepository(_writeContext, _readContext, _IMapper);
        public IResumeRepository ResumeRepository => _resumeRepository ??= new ResumeRepository(_writeContext, _readContext);
        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_writeContext, _readContext);
        public IAdvertisementRepository AdvertisementRepository => _advertisementRepository ??= new AdvertisementRepository(_writeContext, _readContext);
        public ICompanyRepository CompanyRepository => _companyRepository ??= new CompanyRepository(_writeContext, _readContext);

        public ILogsRepository LogsRepository => _logsRepository ??= new LogsRepository(_writeContext);

        public ICompanyBenefitsReposity CompanyBenefitsReposity => _companyBenefitsReposity ??= new CompanyBenefitsReposity(_writeContext, _readContext);

        public ITermsSectionsRepository TermsSectionsRepository => _termsSectionsRepository ??= new TermsSectionsRepository(_writeContext, _readContext);

        public ITermsOfServicesRepository TermsOfServicesRepository => _termsOfServicesRepository ??= new TermsOfServicesRepository(_writeContext, _readContext);

        public ILanguagesRepository LanguagesRepository => _languagesRepository ??= new LanguagesRepository(_writeContext, _readContext);

        public IWorkExperiencesRepository WorkExperiencesRepository => _workExperiencesRepository ??= new WorkExperiencesRepository(_writeContext, _readContext);

        public IEducationsRepository EducationsRepository => _educationsRepository ??= new EducationsRepository(_writeContext, _readContext);

        public ISkillsRepository SkillsRepository => _skillsRepository ??= new SkillsRepository(_writeContext, _readContext);

        public IPricingFeaturesRepository PricingFeaturesRepository => _pricingFeaturesRepository ??= new PricingFeaturesRepository(_writeContext, _readContext);

        public IPricingCategoriesRepository PricingCategoriesRepository => _pricingCategoriesRepository ??= new PricingCategoriesRepository(_writeContext, _readContext);

        public IPricingPlansRepository PricingPlansRepository => _pricingPlansRepository ??= new PricingPlansRepository(_writeContext, _readContext);

        public IFaqQuestionsRepository FaqQuestionsRepository => _faqQuestionsRepository ??= new FaqQuestionsRepository(_writeContext, _readContext);

        public IFaqCategoriesRepository FaqCategoriesRepository => _faqCategoriesRepository ??= new FaqCategoriesRepository(_writeContext, _readContext);

        public ICustomerAddressesRepository CustomerAddressesRepository => _customerAddressesRepository ??= new CustomerAddressesRepository(_writeContext, _readContext);

        public IFeaturesRepository FeaturesRepository => _featuresRepository ??= new FeaturesRepository(_writeContext, _readContext);

        public IJobPostsRepository JobPostsRepository => _jobPostsRepository ??= new JobPostsRepository(_writeContext, _readContext);

        //public IJobOffersRepository JobOffersRepository => _jobOffersRepository ??= new JobOffersRepository(_writeContext, _readContext);

        public IJobRequestsRepository JobRequestsRepository => _jobRequestsRepository ??= new JobRequestsRepository(_writeContext, _readContext);

        public IJobsRepository JobsRepository => _jobsRepository ??= new JobsRepository(_writeContext, _readContext);

        public ICompanyRepository companyRepository => _companyRepository ??= new CompanyRepository(_writeContext, _readContext);

        public ICandidateJobPreferences candidateJobPreferences => _candidateJobPreferences ??= new CandidateJobPreferencesRepository(_writeContext, _readContext);

        public ICompanyJobPreferencesRepository companyJobPreferences => _companyJobPreferences ??= new CompanyJobPreferencesRepository(_writeContext, _readContext);
        public IJobTestAssignment jobTestAssignment => _jobTestAssignment ??= new JobTestAssignmentRepository(_writeContext, _readContext);

        public IPersonalityTestItem personalityTestItem => _personalityTestItem ??= new PersonalityTestItemRepository(_writeContext, _readContext);

        public IPersonalityTestResponse personalityTestResponse => _personalityTestResponse ??= new PersonalityTestResponseRepository(_writeContext, _readContext);

        public IPersonalityTestResult personalityTestResult => _personalityTestResult ??= new PersonalityTestResultRepository(_writeContext, _readContext);

        public IPersonalityTrait personalityTrait => _personalityTrait ??= new PersonalityTraitRepository(_writeContext, _readContext);

        public IPsychologyTest psychologyTest => _psychologyTest ??= new PsychologyTestRepository(_writeContext, _readContext);

        public IPsychologyTestQuestion psychologyTestQuestion => _psychologyTestQuestion ??= new PsychologyTestQuestionRepository(_writeContext, _readContext);

        public IPsychologyTestResponse psychologyTestResponse => _psychologyTestResponse ??= new PsychologyTestResponseRepository(_writeContext, _readContext);

        public IPsychologyTestResult psychologyTestResult => _psychologyTestResult ??= new PsychologyTestResultRepository(_writeContext, _readContext);

        public IUsersRepository UsersRepository => _usersRepository ??= new UsersRepository(_writeContext, _readContext, _accountService);

        public IPaymentsRepository PaymentsRepository => _paymentsRepository ??= new PaymentsRepository(_writeContext, _readContext);

        public IProductInventories ProductInventories => _productInventories ??= new ProductInventoriesRepository(_writeContext, _readContext);

        public IProductInventorySnapshots ProductInventorySnapshots => _productInventorySnapshots ??= new ProductInventorySnapshotsRepository(_writeContext, _readContext);

        public ISalesOrderItems SalesOrderItems => _salesOrderItems ??= new SalesOrderItemsRepository(_writeContext, _readContext);

        public ISalesOrders SalesOrders => _salesOrders ??= new SalesOrdersRepository(_writeContext, _readContext);

        public IInterviewDetails InterviewDetails => _interviewDetails ??= new InterviewDetailsRepository(_writeContext, _readContext);

        public IJobApplication JobApplication => _jobApplication ??= new JobApplicationRepository(_writeContext, _readContext);

        public IOfferDetails OfferDetails => _offerDetails ??= new OfferDetailsRepository(_writeContext, _readContext);

        public IRejectionDetails RejectionDetails => _rejectionDetails ??= new RejectionDetailsRepository(_writeContext, _readContext);

        public ISubmissionDetails SubmissionDetails => _submissionDetails ??= new SubmissionDetailsRepository(_writeContext, _readContext);

        public IUserSettingRepository UserSettingRepository => _userSettingRepository ??= new UserSettingRepository(_IMapper, _writeContext, _readContext);

        public ISavedJobRepository SavedJob => _savedJob ??= new SavedJobRepository(_IMapper, _writeContext, _readContext);

        public ICompanyFollowRepository CompanyFollowRepository => _companyFollowRepository ??= new CompanyFollowRepository(_IMapper, _writeContext, _readContext);
        public IBlogRepository BlogRepository => _blogRepository ??= new BlogRepository(_IMapper, _writeContext, _readContext);

        public IAsnwerRepository AnswerRepository => _asnwerRepository ??= new AsnwerRepository(_IMapper, _writeContext, _readContext);

        public IMBTIResultAnswersRepository MBTIResultAnswersRepository => _mbtiResultAnswersRepository ??= new MBTIResultAnswersRepository(_IMapper, _writeContext, _readContext);

        public IPsychologyTestResponseAnswer psychologyTestResponseAnswer => _psychologyTestResponseAnswer ??= new PsychologyTestResponseAnswerRepository(_IMapper, _writeContext, _readContext);

        public IPsychologyTestResultAnswer psychologyTestResultAnswer => _psychologyTestResultAnswer ??= new PsychologyTestResultAnswerRepository(_IMapper, _writeContext, _readContext);

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            int writeResult = await _writeContext.SaveChangesAsync(cancellationToken);
            int readResult = await _readContext.SaveChangesAsync(cancellationToken);
            return writeResult + readResult;
        }

        public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            _transaction = await _writeContext.Database.BeginTransactionAsync(cancellationToken);
        }

        public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction?.CommitAsync(cancellationToken);
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _writeContext.Dispose();
                    _readContext?.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }

}
