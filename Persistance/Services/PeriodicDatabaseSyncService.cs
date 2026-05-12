using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistance.DatabaseContext.ReadDbContext;
using Persistance.DatabaseContext.WriteDbContext;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq; // Added for Skip, Take, Count, and Any
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Components.Web;
using Persistance.Interfaces;


namespace Persistance.Services
{

    public class PeriodicDatabaseSyncService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PeriodicDatabaseSyncService> _logger;
        private readonly TimeSpan _interval = TimeSpan.FromMinutes(2);

        //public PeriodicDatabaseSyncService(IServiceProvider serviceProvider, ILogger<PeriodicDatabaseSyncService> logger)
        //{
        //    _serviceProvider = serviceProvider;
        //    _logger = logger;
        //}
        public PeriodicDatabaseSyncService(IServiceScopeFactory serviceScopeFactory, ILogger<PeriodicDatabaseSyncService> logger)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("PeriodicDatabaseSyncService started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var syncService = scope.ServiceProvider.GetRequiredService<GenericDatabaseSyncService>();

                    var entityTypes = new[]
                    {
                        typeof(Province), typeof(Category), typeof(FaqCategory), typeof(Language),
                        typeof(MBTIResult), typeof(MBTIQuestion), typeof(MenuItem), typeof(TechnicalOption),
                        typeof(TermsOfService), typeof(TermsSection), typeof(PricingCategory), typeof(PricingPlan),
                        typeof(PricingFeature), typeof(Feature), typeof(CompanyBenefit), typeof(JobCategory), typeof(City),
                        typeof(User), typeof(CustomerAddress), typeof(Customer), typeof(Company),
                        typeof(Advertisement), typeof(Candidate), typeof(Education), typeof(FaqQuestion),
                        typeof(Job), typeof(JobPost), typeof(JobRequest), typeof(Order), typeof(Payment),
                        typeof(Product), typeof(ProductInventory), typeof(ProductInventorySnapshot),
                        typeof(Resume), typeof(SalesOrder), typeof(SalesOrderItem), typeof(Skill),
                        typeof(WorkExperience), typeof(PersonalityTrait), typeof(PersonalityTestItem),
                        typeof(PersonalityTestResponse), typeof(PersonalityTestResult), typeof(PsychologyTest),
                        typeof(PsychologyTestQuestion), typeof(PsychologyTestResponse), typeof(PsychologyTestResult),
                        typeof(JobTestAssignment), typeof(CompanyFollow), typeof(UserSetting), typeof(JobApplication),
                        typeof(InterviewDetail), typeof(OfferDetails), typeof(RejectionDetails), typeof(SubmissionDetails),
                        typeof(CandidateJobPreferences), typeof(CompanyJobPreferences), typeof(Logs), typeof(PushSubscription)
                    };

                    _logger.LogInformation("Starting database synchronization at {Time}", DateTime.Now);
                    await syncService.SyncEntitiesAsync(
                        entityTypes,
                        incremental: true,
                        lastSyncTime: DateTime.Now.AddHours(-24),
                        batchSize: 1000,
                        cancellationToken: stoppingToken);
                    _logger.LogInformation("Database synchronization completed at {Time}", DateTime.Now);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during database synchronization at {Time}", DateTime.Now);
                }

                await Task.Delay(_interval, stoppingToken);
            }

            _logger.LogInformation("PeriodicDatabaseSyncService stopped.");
        }
    }
    public class GenericDatabaseSyncService
    {
        private readonly ILogger<GenericDatabaseSyncService> _logger;
        private readonly Dictionary<Type, Func<object, object>> _entityMappers;
        private static readonly Dictionary<Type, string[]> _entityIncludes = new Dictionary<Type, string[]>();
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IDbContextFactory<WriteDbContext> _writeFactory;
        private readonly IDbContextFactory<ReadDbContext> _readFactory;

        public GenericDatabaseSyncService(
            IDbContextFactory<WriteDbContext> writeFactory,
            IDbContextFactory<ReadDbContext> readFactory,
            IServiceScopeFactory serviceScopeFactory,
            ILogger<GenericDatabaseSyncService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _writeFactory = writeFactory;
            _readFactory = readFactory;
            // Initialize entity includes with duplicate key prevention
            void AddEntityInclude(Type type, string[] includes)
            {
                if (!_entityIncludes.ContainsKey(type))
                {
                    _entityIncludes.Add(type, includes);
                }
                else
                {
                    _logger.LogWarning("Duplicate entity include attempted for {EntityType}. Skipping addition.", type.Name);
                }
            }

            AddEntityInclude(typeof(Category), new string[] { });
            AddEntityInclude(typeof(FaqCategory), new string[] { });
            AddEntityInclude(typeof(JobCategory), new string[] { });
            AddEntityInclude(typeof(PricingCategory), new string[] { });
            AddEntityInclude(typeof(Province), new string[] { });
            AddEntityInclude(typeof(TechnicalOption), new string[] { });
            AddEntityInclude(typeof(TermsOfService), new string[] { });
            AddEntityInclude(typeof(Feature), new[] { "Jobs" });
            AddEntityInclude(typeof(User), new string[] { });
            AddEntityInclude(typeof(PricingFeature), new[] { "PricingPlan" });
            AddEntityInclude(typeof(TermsSection), new[] { "TermsOfService" });
            AddEntityInclude(typeof(PricingPlan), new[] { "PricingCategory" });
            AddEntityInclude(typeof(City), new[] { "Province" });
            AddEntityInclude(typeof(Company), new string[] { "Benefits" });
            AddEntityInclude(typeof(CompanyBenefit), new string[] { }); // Removed invalid 'Company' navigation
            AddEntityInclude(typeof(FaqQuestion), new[] { "FaqCategory" });
            AddEntityInclude(typeof(Product), new[] { "Category" });
            AddEntityInclude(typeof(Advertisement), new[] { "Staff", "Category", "Company" });
            AddEntityInclude(typeof(ProductInventory), new[] { "Product" });
            AddEntityInclude(typeof(ProductInventorySnapshot), new[] { "Product" });
            AddEntityInclude(typeof(Order), new[] { "User", "PricingPlan" });
            AddEntityInclude(typeof(Customer), new[] { "Orders" });
            AddEntityInclude(typeof(CustomerAddress), new[] { "Customer" });
            AddEntityInclude(typeof(SalesOrder), new[] { "Customer" });
            AddEntityInclude(typeof(SalesOrderItem), new[] { "Product", "SalesOrder" });
            AddEntityInclude(typeof(Payment), new[] { "Advertisement" });
            AddEntityInclude(typeof(Candidate), new[] { "User", "City", "Job" });
            AddEntityInclude(typeof(JobPost), new[] { "Staff", "City", "Job" });
            AddEntityInclude(typeof(JobRequest), new[] { "User", "JobPost" });
            AddEntityInclude(typeof(Resume), new[] { "User" });
            AddEntityInclude(typeof(Education), new[] { "Resume" });
            AddEntityInclude(typeof(Language), new[] { "Resume" });
            AddEntityInclude(typeof(Skill), new[] { "Candidates", "JobPosts", "Resume" });
            AddEntityInclude(typeof(WorkExperience), new[] { "Resume" });
            AddEntityInclude(typeof(MBTIResult), new[] { "Users" });
            AddEntityInclude(typeof(MBTIQuestion), new[] { "MBTIResult" });
            AddEntityInclude(typeof(PersonalityTestItem), new[] { "PersonalityTrait" });
            AddEntityInclude(typeof(PersonalityTestResponse), new[] { "User", "PersonalityTestItem" });
            AddEntityInclude(typeof(PersonalityTestResult), new[] { "User" });
            AddEntityInclude(typeof(PsychologyTestQuestion), new[] { "PsychologyTest" });
            AddEntityInclude(typeof(PsychologyTestResponse), new[] { "User", "PsychologyTest", "PsychologyTestQuestion" });
            AddEntityInclude(typeof(PsychologyTestResult), new[] { "User", "PsychologyTest" });
            AddEntityInclude(typeof(JobTestAssignment), new[] { "Job", "PsychologyTest", "PersonalityTest" });
            AddEntityInclude(typeof(CompanyFollow), new[] { "Company", "User" });
            AddEntityInclude(typeof(UserSetting), new[] { "User" });
            AddEntityInclude(typeof(JobApplication), new[] { "Job", "Candidate" });
            AddEntityInclude(typeof(InterviewDetail), new[] { "JobApplication" });
            AddEntityInclude(typeof(OfferDetails), new[] { "JobApplication" });
            AddEntityInclude(typeof(RejectionDetails), new[] { "JobApplication" });
            AddEntityInclude(typeof(SubmissionDetails), new[] { "JobApplication" });
            AddEntityInclude(typeof(CandidateJobPreferences), new[] { "User", "City", "JobCategory" });
            AddEntityInclude(typeof(CompanyJobPreferences), new[] { "JobPost" });
            AddEntityInclude(typeof(Logs), new string[] { });
            AddEntityInclude(typeof(PushSubscription), new string[] { });

            _entityMappers = new Dictionary<Type, Func<object, object>>
            {
                { typeof(Province), entity => entity },
                { typeof(Category), entity => entity },
                { typeof(FaqCategory), entity => entity },
                { typeof(JobCategory), entity => entity },
                { typeof(PricingCategory), entity => entity },
                { typeof(TechnicalOption), entity => entity },
                { typeof(TermsOfService), entity => entity },
                { typeof(User), entity => entity },
                { typeof(MenuItem), entity => entity },
                { typeof(TermsSection), entity => entity },
                { typeof(PricingPlan), entity => entity },
                { typeof(City), entity => entity },
                { typeof(Company), entity => entity },
                { typeof(CompanyBenefit), entity => entity },
                { typeof(FaqQuestion), entity => entity },
                { typeof(Feature), entity => entity },
                { typeof(PricingFeature), entity => entity },
                { typeof(Product), entity => entity },
                { typeof(Advertisement), entity => entity },
                { typeof(ProductInventory), entity => entity },
                { typeof(ProductInventorySnapshot), entity => entity },
                { typeof(Order), entity => entity },
                { typeof(Customer), entity => entity },
                { typeof(CustomerAddress), entity => entity },
                { typeof(SalesOrder), entity => entity },
                { typeof(SalesOrderItem), entity => entity },
                { typeof(Payment), entity => entity },
                { typeof(Candidate), entity => entity },
                { typeof(JobPost), entity => entity },
                { typeof(JobRequest), entity => entity },
                { typeof(Resume), entity => entity },
                { typeof(Education), entity => entity },
                { typeof(Language), entity => entity },
                { typeof(Skill), entity => entity },
                { typeof(WorkExperience), entity => entity },
                { typeof(MBTIResult), entity => entity },
                { typeof(MBTIQuestion), entity => entity },
                { typeof(PersonalityTestItem), entity => entity },
                { typeof(PersonalityTestResponse), entity => entity },
                { typeof(PersonalityTestResult), entity => entity },
                { typeof(PsychologyTest), entity => entity },
                { typeof(PsychologyTestQuestion), entity => entity },
                { typeof(PsychologyTestResponse), entity => entity },
                { typeof(PsychologyTestResult), entity => entity },
                { typeof(JobTestAssignment), entity => entity },
                { typeof(CompanyFollow), entity => entity },
                { typeof(UserSetting), entity => entity },
                { typeof(JobApplication), entity => entity },
                { typeof(InterviewDetail), entity => entity },
                { typeof(OfferDetails), entity => entity },
                { typeof(RejectionDetails), entity => entity },
                { typeof(SubmissionDetails), entity => entity },
                { typeof(CandidateJobPreferences), entity => entity },
                { typeof(CompanyJobPreferences), entity => entity },
                { typeof(Logs), entity => entity },
                { typeof(PushSubscription), entity => entity }
            };
        }

        public async Task SyncEntitiesAsync(
            IEnumerable<Type> entityTypes,
            bool incremental = false,
            DateTime? lastSyncTime = null,
            int batchSize = 1000,
            CancellationToken cancellationToken = default)
        {
            var orderedEntityTypes = OrderEntityTypesByDependencies(entityTypes);
            var idMapping = new Dictionary<Type, Dictionary<object, object>>();
            foreach (var entityType in orderedEntityTypes)
            {
                try
                {
                    var method = GetType().GetMethod(nameof(SyncEntityTypeAsync), BindingFlags.NonPublic | BindingFlags.Instance);
                    var genericMethod = method.MakeGenericMethod(entityType);
                    await (Task)genericMethod.Invoke(this, new object[] { entityType, incremental, lastSyncTime, batchSize, cancellationToken, idMapping });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during synchronization for {EntityType}.", entityType.Name);
                    continue;
                }
            }
        }

        private async Task SyncEntityTypeAsync<T>(
            Type entityType,
            bool incremental,
            DateTime? lastSyncTime,
            int batchSize,
            CancellationToken cancellationToken,
            Dictionary<Type, Dictionary<object, object>> idMapping)
            where T : class
        {
            using var scope = _serviceScopeFactory.CreateScope();
            _logger.LogInformation("Syncing entity type: {EntityType}", entityType.Name);

            //var writeDbContext = scope.ServiceProvider.GetRequiredService<WriteDbContext>();
            //var readDbContext = scope.ServiceProvider.GetRequiredService<ReadDbContext>();
            await using var writeDbContext = await _writeFactory.CreateDbContextAsync(cancellationToken);
            await using var readDbContext = await _readFactory.CreateDbContextAsync(cancellationToken);
            var readDbSet = readDbContext.Set<T>();
            _logger.LogInformation("ReadDb connection string: {conn}", readDbContext.Database.GetDbConnection().ConnectionString);

            if (!idMapping.ContainsKey(entityType))
            {
                idMapping[entityType] = new Dictionary<object, object>();
            }
            var typeIdMapping = idMapping[entityType];

            IQueryable<T> query = writeDbContext.Set<T>().AsNoTracking();

            if (_entityIncludes.TryGetValue(entityType, out var includes))
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (incremental && lastSyncTime.HasValue)
            {
                var beforeFilterCount = await query.CountAsync(cancellationToken);
                _logger.LogInformation("Records before applying WhereDynamic for {EntityType}: {Count}", entityType.Name, beforeFilterCount);

                // Added debug logging to inspect timestamps
                if (beforeFilterCount > 0)
                {
                    var sampleRecords = await query.Take(5).ToListAsync(cancellationToken);
                    foreach (var record in sampleRecords)
                    {
                        var id = record.GetType().GetProperty("Id")?.GetValue(record);
                        var createdDate = record.GetType().GetProperty("CreatedDate")?.GetValue(record) ?? record.GetType().GetProperty("DateCreated")?.GetValue(record);
                        var lastModifiedDate = record.GetType().GetProperty("LastModifiedDate")?.GetValue(record) ?? record.GetType().GetProperty("DateModified")?.GetValue(record);
                        _logger.LogDebug("Sample record for {EntityType}: Id={Id}, CreatedDate={CreatedDate}, LastModifiedDate={LastModifiedDate}",
                            entityType.Name, id, createdDate, lastModifiedDate);
                    }
                }

                _logger.LogInformation("Applying incremental filter for {EntityType} with lastSyncTime: {LastSyncTime}", entityType.Name, lastSyncTime.Value);
                query = WhereDynamic(query, entityType, "LastModifiedDate", ">=", lastSyncTime.Value);

                var afterFilterCount = await query.CountAsync(cancellationToken);
                _logger.LogInformation("Records after applying WhereDynamic for {EntityType}: {Count}", entityType.Name, afterFilterCount);
            }

            var idProp = entityType.GetProperty("Id");
            if (idProp != null)
            {
                var parameter = Expression.Parameter(entityType, "e");
                var propertyAccess = Expression.Property(parameter, idProp);
                var orderByLambda = Expression.Lambda(propertyAccess, parameter);
                var orderByMethod = typeof(Queryable).GetMethods()
                    .First(m => m.Name == "OrderBy" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(entityType, idProp.PropertyType);
                query = (IQueryable<T>)orderByMethod.Invoke(null, new object[] { query, orderByLambda });
            }

            int totalRecords = await query.CountAsync(cancellationToken);
            _logger.LogInformation("Total records to sync for {EntityType}: {TotalRecords}", entityType.Name, totalRecords);

            int processed = 0;
            while (processed < totalRecords)
            {
                _logger.LogInformation("Processing batch for {EntityType}, offset: {Processed}, batchSize: {BatchSize}", entityType.Name, processed, batchSize);
                var batch = await query.Skip(processed).Take(batchSize).ToListAsync(cancellationToken);
                _logger.LogInformation("Retrieved batch for {EntityType}: {BatchSize} records", entityType.Name, batch.Count);

                if (!batch.Any()) break;

                var mappedBatch = batch
                    .Select(e => _entityMappers.GetValueOrDefault(entityType, x => x)(e))
                    .Cast<T>()
                    .ToList();

                _logger.LogInformation("Mapped batch for {EntityType}: {MappedBatchSize} records", entityType.Name, mappedBatch.Count);

                try
                {
                    foreach (var item in mappedBatch)
                    {
                        var writeId = idProp?.GetValue(item);
                        if (writeId == null)
                        {
                            _logger.LogWarning("Entity {EntityType} with null Id encountered, skipping.", entityType.Name);
                            continue;
                        }

                        T existing = null;

                        // Special handling for User (by Email)
                        if (entityType == typeof(User))
                        {
                            var emailProp = entityType.GetProperty("Email");
                            var emailValue = emailProp?.GetValue(item)?.ToString();

                            if (!string.IsNullOrEmpty(emailValue))
                            {
                                var parameter = Expression.Parameter(entityType, "e");
                                var nameAccess = Expression.Property(parameter, emailProp);
                                var nameConstant = Expression.Constant(emailValue);
                                var nameEqual = Expression.Equal(nameAccess, nameConstant);
                                var lambda = Expression.Lambda<Func<T, bool>>(nameEqual, parameter);

                                existing = await readDbSet.FirstOrDefaultAsync(lambda, cancellationToken);
                            }
                        }
                        // Special handling for Company (by Name)
                        else if (entityType == typeof(Company))
                        {
                            var nameProp = entityType.GetProperty("Name");
                            var nameValue = nameProp?.GetValue(item)?.ToString();

                            if (!string.IsNullOrEmpty(nameValue))
                            {
                                var parameter = Expression.Parameter(entityType, "e");
                                var nameAccess = Expression.Property(parameter, nameProp);
                                var nameConstant = Expression.Constant(nameValue);
                                var nameEqual = Expression.Equal(nameAccess, nameConstant);
                                var lambda = Expression.Lambda<Func<T, bool>>(nameEqual, parameter);

                                existing = await readDbSet.FirstOrDefaultAsync(lambda, cancellationToken);
                            }
                        }
                        // Default: Find by Id
                        else
                        {
                            if (writeId != null)
                            {
                                existing = await readDbSet.FindAsync(new object[] { writeId }, cancellationToken);
                            }
                        }

                        if (existing != null)
                        {
                            readDbContext.Entry(existing).CurrentValues.SetValues(item);
                            typeIdMapping[writeId] = idProp.GetValue(existing);
                            await UpdateRelatedEntitiesAsync(item, existing, entityType, readDbContext, idMapping, cancellationToken);
                            _logger.LogDebug("Updated existing {EntityType} with Id: {WriteId}", entityType.Name, writeId);
                        }
                        else
                        {
                            await UpdateRelatedEntitiesAsync(item, null, entityType, readDbContext, idMapping, cancellationToken);

                            // Removed Id reset for Company to maintain consistency
                            if (entityType != typeof(Company) && idProp != null && (idProp.PropertyType == typeof(int) || idProp.PropertyType == typeof(long)))
                            {
                                idProp.SetValue(item, 0);
                            }
                            else if (idProp != null && idProp.PropertyType == typeof(Guid))
                            {
                                idProp.SetValue(item, Guid.Empty);
                            }

                            await readDbSet.AddAsync(item, cancellationToken);
                            await readDbContext.SaveChangesAsync(cancellationToken);
                            var readId = idProp.GetValue(item);
                            typeIdMapping[writeId] = readId;
                            _logger.LogDebug("Inserted new {EntityType} with WriteDB Id {WriteId} to ReadDB Id {ReadId}", entityType.Name, writeId, readId);
                        }
                    }

                    await readDbContext.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation("Saved batch for {EntityType}", entityType.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to upsert batch for {EntityType}", entityType.Name);
                    throw;
                }

                processed += batch.Count;
            }
        }

        private async Task UpdateRelatedEntitiesAsync(
            object sourceEntity,
            object targetEntity,
            Type entityType,
            ReadDbContext readDbContext,
            Dictionary<Type, Dictionary<object, object>> idMapping,
            CancellationToken cancellationToken)
        {
            var navigations = readDbContext.Model.FindEntityType(entityType)?.GetNavigations() ?? Enumerable.Empty<INavigation>();
            var foreignKeys = readDbContext.Model.FindEntityType(entityType)?.GetForeignKeys() ?? Enumerable.Empty<IForeignKey>();

            foreach (var fk in foreignKeys)
            {
                var principalType = fk.PrincipalEntityType.ClrType;
                var principalKeyProp = fk.PrincipalKey.Properties[0].PropertyInfo;
                var dependentKeyProp = fk.Properties[0].PropertyInfo;

                var dependentValue = dependentKeyProp.GetValue(sourceEntity);
                if (dependentValue != null && idMapping.TryGetValue(principalType, out var typeIdMapping) && typeIdMapping.TryGetValue(dependentValue, out var mappedId))
                {
                    dependentKeyProp.SetValue(targetEntity ?? sourceEntity, mappedId);
                    _logger.LogDebug("Updated foreign key {PropertyName} from {OldId} to {NewId} for {EntityType}",
                        dependentKeyProp.Name, dependentValue, mappedId, entityType.Name);
                }
                else if (dependentValue != null)
                {
                    var principalEntity = await readDbContext.FindAsync(principalType, new object[] { dependentValue }, cancellationToken);
                    if (principalEntity != null)
                    {
                        dependentKeyProp.SetValue(targetEntity ?? sourceEntity, dependentValue);
                        if (!idMapping.ContainsKey(principalType))
                        {
                            idMapping[principalType] = new Dictionary<object, object>();
                        }
                        idMapping[principalType][dependentValue] = dependentValue;
                    }
                    else
                    {
                        _logger.LogWarning("Principal entity {PrincipalType} with Id {Id} not found in ReadDB, setting foreign key to null for {EntityType}",
                            principalType.Name, dependentValue, entityType.Name);
                        dependentKeyProp.SetValue(targetEntity ?? sourceEntity, null);
                    }
                }
            }

            foreach (var navigation in navigations)
            {
                var navigationProperty = navigation.PropertyInfo;
                var relatedEntity = navigationProperty.GetValue(sourceEntity);
                if (relatedEntity == null) continue;

                var relatedEntityType = navigationProperty.PropertyType;
                if (navigation.IsCollection)
                {
                    var collection = relatedEntity as IEnumerable<object>;
                    if (collection == null) continue;

                    var relatedEntities = collection.ToList();
                    var relatedKeyProp = relatedEntityType.GetProperty("Id");
                    if (relatedKeyProp == null) continue;

                    var newCollection = Activator.CreateInstance(navigationProperty.PropertyType);
                    var addMethod = navigationProperty.PropertyType.GetMethod("Add");

                    foreach (var relatedItem in relatedEntities)
                    {
                        var relatedId = relatedKeyProp.GetValue(relatedItem);
                        if (idMapping.TryGetValue(relatedEntityType, out var typeIdMapping) && typeIdMapping.TryGetValue(relatedId, out var mappedId))
                        {
                            var trackedEntity = await readDbContext.FindAsync(relatedEntityType, new object[] { mappedId }, cancellationToken);
                            if (trackedEntity != null)
                            {
                                addMethod.Invoke(newCollection, new[] { trackedEntity });
                            }
                        }
                    }

                    navigationProperty.SetValue(targetEntity ?? sourceEntity, newCollection);
                }
                else
                {
                    var relatedKeyProp = relatedEntityType.GetProperty("Id");
                    if (relatedKeyProp == null) continue;

                    var relatedId = relatedKeyProp.GetValue(relatedEntity);
                    if (idMapping.TryGetValue(relatedEntityType, out var typeIdMapping) && typeIdMapping.TryGetValue(relatedId, out var mappedId))
                    {
                        var trackedEntity = await readDbContext.FindAsync(relatedEntityType, new object[] { mappedId }, cancellationToken);
                        if (trackedEntity != null)
                        {
                            navigationProperty.SetValue(targetEntity ?? sourceEntity, trackedEntity);
                        }
                    }
                }
            }
        }

        private IEnumerable<Type> OrderEntityTypesByDependencies(IEnumerable<Type> entityTypes)
        {
            var dependencyOrder = new List<(Type EntityType, Type[] DependsOn)>
            {
                (typeof(Category), Array.Empty<Type>()),
                (typeof(FaqCategory), Array.Empty<Type>()),
                (typeof(JobCategory), Array.Empty<Type>()),
                (typeof(PricingCategory), Array.Empty<Type>()),
                (typeof(Province), Array.Empty<Type>()),
                (typeof(TechnicalOption), Array.Empty<Type>()),
                (typeof(TermsOfService), Array.Empty<Type>()),
                (typeof(User), Array.Empty<Type>()),
                (typeof(MenuItem), Array.Empty<Type>()),
                (typeof(TermsSection), new[] { typeof(TermsOfService) }),
                (typeof(PricingPlan), new[] { typeof(PricingCategory) }),
                (typeof(City), new[] { typeof(Province) }),
                (typeof(Company), Array.Empty<Type>()),
                (typeof(CompanyBenefit), new[] { typeof(Company) }),
                (typeof(FaqQuestion), new[] { typeof(FaqCategory) }),
                (typeof(Feature), new[] { typeof(Job) }),
                (typeof(PricingFeature), new[] { typeof(PricingPlan) }),
                (typeof(Product), new[] { typeof(Category) }),
                (typeof(Advertisement), new[] { typeof(User), typeof(Category), typeof(Company) }),
                (typeof(Job), new[] { typeof(User), typeof(City), typeof(Company), typeof(JobCategory) }),
                (typeof(ProductInventory), new[] { typeof(Product) }),
                (typeof(ProductInventorySnapshot), new[] { typeof(Product) }),
                (typeof(Order), new[] { typeof(User), typeof(PricingPlan) }),
                (typeof(Customer), new[] { typeof(Order) }),
                (typeof(CustomerAddress), new[] { typeof(Customer) }),
                (typeof(SalesOrder), new[] { typeof(Customer) }),
                (typeof(SalesOrderItem), new[] { typeof(Product), typeof(SalesOrder) }),
                (typeof(Payment), new[] { typeof(Advertisement), typeof(User), typeof(Order) }),
                (typeof(Candidate), new[] { typeof(User), typeof(City), typeof(Job) }),
                (typeof(JobPost), new[] { typeof(User), typeof(City), typeof(Job) }),
                (typeof(JobRequest), new[] { typeof(User), typeof(JobPost) }),
                (typeof(Resume), new[] { typeof(User) }),
                (typeof(Education), new[] { typeof(Resume) }),
                (typeof(Language), new[] { typeof(Resume) }),
                (typeof(Skill), new[] { typeof(Candidate), typeof(JobPost), typeof(Resume) }),
                (typeof(WorkExperience), new[] { typeof(Resume) }),
                (typeof(MBTIResult), new[] { typeof(User) }),
                (typeof(MBTIQuestion), new[] { typeof(MBTIResult) }),
                (typeof(PersonalityTrait), Array.Empty<Type>()),
                (typeof(PersonalityTestItem), new[] { typeof(PersonalityTrait) }),
                (typeof(PersonalityTestResponse), new[] { typeof(User), typeof(PersonalityTestItem) }),
                (typeof(PersonalityTestResult), new[] { typeof(User) }),
                (typeof(PsychologyTest), Array.Empty<Type>()),
                (typeof(PsychologyTestQuestion), new[] { typeof(PsychologyTest) }),
                (typeof(PsychologyTestResponse), new[] { typeof(User), typeof(PsychologyTest), typeof(PsychologyTestQuestion) }),
                (typeof(PsychologyTestResult), new[] { typeof(User), typeof(PsychologyTest) }),
                (typeof(JobTestAssignment), new[] { typeof(Job), typeof(PsychologyTest), typeof(PersonalityTestResult) }),
                (typeof(CompanyFollow), new[] { typeof(Company), typeof(User) }),
                (typeof(UserSetting), new[] { typeof(User) }),
                (typeof(JobApplication), new[] { typeof(Job), typeof(Candidate) }),
                (typeof(InterviewDetail), new[] { typeof(JobApplication) }),
                (typeof(OfferDetails), new[] { typeof(JobApplication) }),
                (typeof(RejectionDetails), new[] { typeof(JobApplication) }),
                (typeof(SubmissionDetails), new[] { typeof(JobApplication) }),
                (typeof(CandidateJobPreferences), new[] { typeof(User), typeof(City), typeof(JobCategory) }),
                (typeof(CompanyJobPreferences), new[] { typeof(JobPost) }),
                (typeof(Logs), Array.Empty<Type>()),
                (typeof(PushSubscription), Array.Empty<Type>())
            };

            var orderedTypes = new List<Type>();
            var inputTypes = entityTypes.ToHashSet();

            foreach (var (entityType, dependsOn) in dependencyOrder)
            {
                if (inputTypes.Contains(entityType) && !orderedTypes.Contains(entityType))
                {
                    foreach (var dep in dependsOn)
                    {
                        if (inputTypes.Contains(dep) && !orderedTypes.Contains(dep))
                        {
                            orderedTypes.Add(dep);
                        }
                    }
                    orderedTypes.Add(entityType);
                }
            }

            orderedTypes.AddRange(inputTypes.Except(orderedTypes));
            return orderedTypes;
        }

        private IQueryable<T> WhereDynamic<T>(
            IQueryable<T> query,
            Type entityType,
            string propertyName,
            string op,
            object value)
            where T : class
        {
            try
            {
                // Use LastModifiedDate for IAuditableEntity
                var targetPropertyName = typeof(IAuditableEntity).IsAssignableFrom(entityType) ? "LastModifiedDate" : propertyName;
                var property = entityType.GetProperty(targetPropertyName);
                if (property == null)
                {
                    _logger.LogWarning("Property {PropertyName} not found on {EntityType}. Returning unfiltered query.", targetPropertyName, entityType.Name);
                    return query;
                }

                var parameter = Expression.Parameter(entityType, "e");
                var propertyAccess = Expression.Property(parameter, property);
                var constant = Expression.Constant(value, value.GetType());

                Expression comparison;
                bool isNullable = Nullable.GetUnderlyingType(property.PropertyType) != null;
                if (isNullable)
                {
                    var notNullCheck = Expression.NotEqual(propertyAccess, Expression.Constant(null));
                    Expression valueAccess = Expression.Property(propertyAccess, "Value");

                    switch (op)
                    {
                        case ">=":
                            comparison = Expression.GreaterThanOrEqual(valueAccess, constant);
                            break;
                        default:
                            throw new NotSupportedException($"Operator {op} not supported.");
                    }

                    comparison = Expression.AndAlso(notNullCheck, comparison);
                }
                else
                {
                    switch (op)
                    {
                        case ">=":
                            comparison = Expression.GreaterThanOrEqual(propertyAccess, constant);
                            break;
                        default:
                            throw new NotSupportedException($"Operator {op} not supported.");
                    }
                }

                var lambda = Expression.Lambda<Func<T, bool>>(comparison, parameter);
                _logger.LogInformation("Applying filter for {EntityType}: {Lambda}", entityType.Name, lambda);

                var beforeCount = query.Count();
                query = query.Where(lambda);
                var afterCount = query.Count();
                _logger.LogDebug("Filter for {EntityType} reduced record count from {BeforeCount} to {AfterCount}", entityType.Name, beforeCount, afterCount);

                return query;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to apply dynamic filter for {EntityType} with and operator {Operator}.", entityType.Name, op);
                throw;
            }
        }
    }

    public static class DbContextExtensions
    {
        public static IQueryable GetDbSet(this DbContext context, Type entityType)
        {
            var dbSetProperty = context.GetType()
                .GetProperties()
                .FirstOrDefault(p => p.PropertyType.IsGenericType &&
                                     p.PropertyType.GetGenericTypeDefinition() == typeof(DbSet<>) &&
                                     p.PropertyType.GetGenericArguments()[0] == entityType);

            if (dbSetProperty == null)
            {
                throw new InvalidOperationException($"No DbSet found for entity type {entityType.Name}");
            }

            return (IQueryable)dbSetProperty.GetValue(context);
        }
    }

    public static class QueryableExtensions
    {
        public static IQueryable<T> SkipTake<T>(this IQueryable<T> source, int skip, int take)
        {
            if (skip > 0)
                source = source.Skip(skip);
            return source.Take(take);
        }
    }

}
