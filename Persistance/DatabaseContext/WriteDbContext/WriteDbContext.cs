using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Persistance.Exceptions;
using Persistance.Extensions;

namespace Persistance.DatabaseContext.WriteDbContext
{
    public class WriteDbContext : IdentityDbContext<IdentityUser> //, IApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        public WriteDbContext(DbContextOptions<WriteDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PushSubscription> PushSubscriptions { get; set; }
        public DbSet<CompanyBenefit> CompanyBenefits { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

        public virtual DbSet<Education> Educations { get; set; }

        #region Frequently Asked Questions and Category  Db Sets

        public virtual DbSet<FaqCategory> FaqCategory { get; set; }
        public virtual DbSet<FaqQuestion> FaqQuestion { get; set; }

        #endregion Frequently Asked Questions and Category Db Sets

        public virtual DbSet<Feature> Features { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }

        #region Jobs Db Sets

        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<SavedJob> SavedJobs { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }

        //public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<JobRequest> JobRequests { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<OfferDetails> OfferDetails { get; set; }
        public DbSet<RejectionDetails> RejectionDetails { get; set; }
        public DbSet<SubmissionDetails> SubmissionDetails { get; set; }
        public DbSet<InterviewDetail> InterviewDetails { get; set; }

        #endregion Jobs Db Sets

        public DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<CompanyFollow> CompanyFollows { get; set; }


        #region Db Sets Tests 
        public virtual DbSet<MBTIQuestion> MBTIQuestions { get; set; }
        public virtual DbSet<MBTIResult> MBTIResults { get; set; }
        public virtual DbSet<MBTIResultAnswer> MBTIResultAnswers { get; set; }

        #endregion

        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }

        #region Pricing Db Sets

        public virtual DbSet<PricingCategory> PricingCategories { get; set; }
        public virtual DbSet<PricingFeature> PricingFeatures { get; set; }
        public virtual DbSet<PricingPlan> PricingPlans { get; set; }

        #endregion Pricing Db Sets
        #region Logs 
        public DbSet<Logs> Logs { get; set; }
        #endregion Logs

        #region Product

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }

        #endregion Product

        public DbSet<Province> Provinces { get; set; }

        public virtual DbSet<Resume> Resumes { get; set; }
        public virtual DbSet<SalesOrder> SalesOrders { get; set; }

        public virtual DbSet<SalesOrderItem> SalesOrderItems { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public DbSet<TechnicalOption> TechnicalOptions { get; set; }
        public DbSet<PsychologyTest> PsychologyTests { get; set; }
        public DbSet<PsychologyTestQuestion> PsychologyTestQuestions { get; set; }
        public DbSet<PsychologyTestResponse> PsychologyTestResponses { get; set; }
        public DbSet<PsychologyTestResult> PsychologyTestResults { get; set; }
        public DbSet<PsychologyTestResponseAnswer> PsychologyTestResponseAnswers { get; set; }
        public DbSet<PsychologyTestResultAnswer> PsychologyTestResultAnswers { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }

        public DbSet<PsychologyTestInterpretation> personalityTestInterpretations { get; set; }
        public DbSet<JobTestAssignment> JobTestAssignments { get; set; }
        public DbSet<PersonalityTrait> PersonalityTraits { get; set; }
        public DbSet<PersonalityTestResult> PersonalityTestResults { get; set; }
        public DbSet<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        public DbSet<PersonalityTestItem> PersonalityTestItems { get; set; }


        public DbSet<TermsOfService> TermsOfServices { get; set; }
        public DbSet<TermsSection> TermsSections { get; set; }


        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<User> Users { get; set; }

        #region IDentity


        #endregion

        #region OnModelCreating
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //modelBuilder.Entity<User>().ToTable("Users");

            //// Map Identity tables explicitly
            //modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            //modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            //modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            //modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            //modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            //modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");

            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            //modelBuilder.Entity<City>()
            //  .HasOne(c => c.Province)
            //  .WithMany(p => p.Cities)
            //  .HasForeignKey(c => c.ProvinceId);

            modelBuilder.Entity<City>().Property(c => c.IsActive)
            .HasDefaultValue(false);



            modelBuilder.Entity<Candidate>()
                .HasOne(candidate => candidate.Job)
                .WithMany(job => job.Candidates)
                .HasForeignKey(candidate => candidate.JobId);

            modelBuilder.Entity<Candidate>().Property(c => c.IsActive)
            .HasDefaultValue(false);


            modelBuilder.Entity<Candidate>()
            .HasOne(c => c.CandidateJobPreferences)
            .WithMany()
            .HasForeignKey(c => c.CandidateJobPreferencesId)
            .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Job)
                .WithMany(j => j.Candidates)
                .HasForeignKey(c => c.JobId)
                .OnDelete(DeleteBehavior.NoAction);




            modelBuilder.Entity<Company>()
                .Property(company => company.Size)
                .HasConversion<string>();

            modelBuilder.Entity<Company>().Property(c => c.IsActive)
             .HasDefaultValue(false);

            modelBuilder.Entity<Company>()
             .HasMany(c => c.Benefits)
             .WithOne() // No navigation property back to Company
             .HasForeignKey(b => b.CompanyId)
             .OnDelete(DeleteBehavior.Cascade); // Optional: cascade delete

            modelBuilder.Entity<CompanyBenefit>()
                .Ignore(b => b.Company);

            modelBuilder.Entity<Company>()
                .HasOne(c => c.User)
                .WithMany() // Assuming User has no navigation property back to Company
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent cascading deletes

            modelBuilder.Entity<Job>()
             .HasOne(job => job.Company)
             .WithMany(company => company.Jobs)
             .HasForeignKey(job => job.CompanyId);

            modelBuilder.Entity<Job>()
               .Property(job => job.Level)
               .HasConversion<string>();

            modelBuilder.Entity<Job>().Property(c => c.IsActive)
            .HasDefaultValue(false);


            //modelBuilder.Entity<JobOffer>(entity =>
            //{
            //    entity.HasKey(e => e.Id);

            //    entity.Property(e => e.Details)
            //        .IsRequired();

            //    entity.Property(e => e.SalaryOffered)
            //        .HasColumnType("decimal(18,2)");

            //    entity.Property(e => e.CreatedAt)
            //        .IsRequired();

            //    entity.Property(e => e.Status)
            //        .IsRequired();

            //    //entity.HasOne(e => e.User)
            //    //    .WithMany(u => u.JobOffers)
            //    //    .HasForeignKey(e => e.UserId)
            //    //    .OnDelete(DeleteBehavior.Cascade);

            //    //entity.HasOne(e => e.JobPost)
            //    //    .WithMany(j => j.JobOffers)
            //    //    .HasForeignKey(e => e.JobPostId)
            //    //    .OnDelete(DeleteBehavior.NoAction); // Change to NoAction or SetNull
            //});
            modelBuilder.Entity<WorkExperience>()
         .HasOne(we => we.Resume)
         .WithMany(r => r.WorkExperiences)
         .HasForeignKey(we => we.ResumeId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(m => m.ParentId);

            // modelBuilder.Entity<Customer>()
            //.HasOne(c => c.PrimaryAddress)
            //.WithOne(a => a.Customers) // Assuming you have a 'Customer' navigation property in CustomerAddress
            //.HasForeignKey<CustomerAddress>(a => a.CustomerId) // 'CustomerId' in CustomerAddress is the FK
            //.IsRequired(false); // Adjust IsRequired based on your business logic


            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasMany(c => c.CustomerAddresses)
                 .WithOne(a => a.Customer)
                 .HasForeignKey(a => a.CustomerId)
                 .IsRequired(false)
                 .OnDelete(DeleteBehavior.Restrict); // Added OnDelete

                // Correctly configure the one-to-many relationship
                entity.HasMany(c => c.CustomerAddresses)
                    .WithOne(a => a.Customer)
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade); //Added OnDelete

                entity.HasOne(c => c.Orders) // Corrected Orders to Order
                   .WithOne(o => o.Customer)
                   .HasForeignKey<Customer>(c => c.OrdersId)
                   .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.HasOne(a => a.Customer)
                    .WithMany(c => c.CustomerAddresses); //This is needed for the other side of the relationship
            });

            modelBuilder.Entity<Education>()
              .HasOne(e => e.Resume)
              .WithMany(r => r.Educations)
              .HasForeignKey(e => e.ResumeId)
              .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Language>()
              .HasOne(l => l.Resume)
              .WithMany(r => r.Languages)
              .HasForeignKey(l => l.ResumeId)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Skill>()
              .HasOne(s => s.Resume)
              .WithMany(r => r.Skills)
              .HasForeignKey(s => s.ResumeId)
              .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            modelBuilder.Entity<Skill>()
                .HasOne(s => s.Candidates)
                .WithMany(c => c.Skill)
                .HasForeignKey(s => s.CandidateId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .IsRequired(false);

            modelBuilder.Entity<JobRequest>()
                 .HasOne(jr => jr.User)
                 .WithMany(u => u.JobRequests)
                 .HasForeignKey(jr => jr.UserId)
                 .OnDelete(DeleteBehavior.Cascade); // Keep cascade for User deletion (common scenario)

            modelBuilder.Entity<JobRequest>()
                .HasOne(jr => jr.JobPost)
                .WithMany(jp => jp.JobRequests)
                .HasForeignKey(jr => jr.JobPostId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Advertisement>()
                   .HasOne(a => a.Company)
                   .WithMany(c => c.Advertisements)
                   .HasForeignKey(a => a.CompanyId)
                   .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Category)
                .WithMany(c => c.Advertisements)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Cascade); // Keep Cascade if needed

            modelBuilder.Entity<Advertisement>()
                .HasOne(a => a.Staff)
                .WithMany(u => u.Advertisements)
                .HasForeignKey(a => a.StaffId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<JobApplication>()
               .HasOne(ja => ja.Job)
               .WithMany(j => j.JobApplications)
               .HasForeignKey(ja => ja.JobId)
               .OnDelete(DeleteBehavior.Restrict); // Change to Restrict or NoAction

            modelBuilder.Entity<CandidateJobPreferences>()
                .Property(e => e.MinSalary)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Company>()
                .Property(e => e.Rating)
                .HasColumnType("decimal(5,2)");





            // Configure PsychologyTestResult and PsychologyTestInterpretation (one-to-many)
            modelBuilder.Entity<PsychologyTestResult>()
                .HasMany(ptr => ptr.Interpretation)
                .WithOne(pti => pti.PsychologyTestResult)
                .HasForeignKey(pti => pti.PsychologyTestResultId)
                .OnDelete(DeleteBehavior.NoAction);

            // Configure PsychologyTestResult relationships
            modelBuilder.Entity<PsychologyTestResult>()
                .HasOne(ptr => ptr.PsychologyTest)
                .WithMany(pt => pt.PsychologyTestResults)
                .HasForeignKey(ptr => ptr.PsychologyTestId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<PsychologyTestResult>()
            //    .HasOne(ptr => ptr.User)
            //    .WithMany()
            //    .HasForeignKey(ptr => ptr.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Configure PsychologyTestResponse relationships
            modelBuilder.Entity<PsychologyTestResponse>()
                .HasOne(ptr => ptr.PsychologyTest)
                .WithMany(pt => pt.PsychologyTestResponses)
                .HasForeignKey(ptr => ptr.PsychologyTestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PsychologyTestResponse>()
                .HasOne(ptr => ptr.PsychologyTestQuestion)
                .WithMany(ptq => ptq.PsychologyTestResponses)
                .HasForeignKey(ptr => ptr.PsychologyTestQuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<PsychologyTestResponse>()
            //    .HasOne(ptr => ptr.TestResult)
            //    .WithMany(ptr => ptr.Responses)
            //    .HasForeignKey(ptr => ptr.TestResultId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<PsychologyTestResponse>()
            //    .HasOne(ptr => ptr.User)
            //    .WithMany()
            //    .HasForeignKey(ptr => ptr.UserId)
            //    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PsychologyTestResponse>()
                 .HasOne(ptr => ptr.TestResult)
                 .WithMany(ptr => ptr.Responses)
                 .HasForeignKey(ptr => ptr.TestResultId)
                 .OnDelete(DeleteBehavior.Restrict);

            // Configure PsychologyTestQuestion relationships
            modelBuilder.Entity<PsychologyTestQuestion>()
                .HasOne(ptq => ptq.PsychologyTest)
                .WithMany(pt => pt.PsychologyTestQuestions)
                .HasForeignKey(ptq => ptq.PsychologyTestId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PsychologyTestResult>()
                .HasMany(ptr => ptr.Interpretation)
                .WithOne(pti => pti.PsychologyTestResult)
                .HasForeignKey(pti => pti.PsychologyTestResultId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Payment>()
              .HasOne(p => p.User)
              .WithMany(u => u.Payments)
              .HasForeignKey(p => p.UserId)
              .OnDelete(DeleteBehavior.Restrict);

            //.HasConstraintName("FK_PsychologyTestResponseAnswers_PsychologyTests_PsychologyTestId");
            AddSpecialUser(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Log(CoreEventId.InvalidIncludePathError));
        }
        #endregion
        #region SaveChanges
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in base.ChangeTracker.Entries<IBaseEntity<Guid>>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                //entry.Entity.IsActive = true;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.IsActive = false;
                }
                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.DateModified = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsActive = false;
                }

            }
            foreach (var entry in base.ChangeTracker.Entries<IBaseEntity<int>>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                //entry.Entity.IsActive = true;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.IsActive = false;
                }
                if (entry.State == EntityState.Modified || entry.HasChangedOwnedEntities())
                {
                    entry.Entity.DateModified = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.Entity.IsActive = false;
                }

            }
            var writeResult = await base.SaveChangesAsync(cancellationToken);
            return writeResult;



        }

        public override int SaveChanges()
        {
            foreach (var entry in base.ChangeTracker.Entries<IBaseEntity<Guid>>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.IsActive = true;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            foreach (var entry in base.ChangeTracker.Entries<IBaseEntity<int>>().Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                entry.Entity.DateModified = DateTime.Now;
                entry.Entity.IsActive = true;
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                }
            }
            // Use a single transaction for both contexts
            using (var transaction = base.Database.BeginTransactionAsync().Result)
            {
                int writeResult = 0;
                try
                {
                    //writeResult = await _writeContext.SaveChangesAsync(cancellationToken);
                    //readResult = await _readContext.SaveChangesAsync(cancellationToken);
                    //writeResult =  base.SaveChanges();
                    // Commit the transaction
                    transaction.CommitAsync();
                    return writeResult;
                }
                catch (DbUpdateException ex)
                {
                    transaction.RollbackAsync();

                    throw new DataBaseExcption("Failed to commit changes to the database.", ex)
                    {
                        ErrorCode = 50001
                    };
                }
                catch (Exception ex)
                {
                    // Log any other exception
                    transaction.RollbackAsync();
                    throw new DataBaseExcption("An unexpected error occurred.", ex)
                    {
                        ErrorCode = 50002
                    };
                }
                finally
                {
                    // No need to dispose the transaction here; the 'using' statement does that.

                }
            }
        }

        #endregion SaveChanges

        #region Createion 
        private void AddSpecialUser(ModelBuilder modelBuilder)
        {
            string adminPassword = _configuration["ADMINPASS:PASSWORD"];
            if (string.IsNullOrEmpty(adminPassword))
            {
                Console.WriteLine(adminPassword);
                Console.WriteLine(_configuration["ADMINPASS:PASSWORD"]);
                throw new Exception($"Admin password is missing from configuration. {adminPassword} {_configuration["ADMINPASS"]}", new Exception("adminPassword Need To Be Added"));
            }
            var user = new User
            {
                Id = "5542BA7C-C896-4500-85F3-2E1E1197122F",
                AccessFailedCount = 0,
                DateCreated = DateTime.Now,
                IsActive = true,
                PhoneNumberConfirmed = true,
                EmailConfirmed = true,
                DateModified = DateTime.Now,
                PhoneNumber = "09125274263",
                FirstName = "Behzad",
                LastName = "Eskandari",
                UserName = "behzad.b.i.g@gmail.com",
                NormalizedUserName = "BEHZAD",
                Email = "behzad.b.i.g@gmail.com",
                NormalizedEmail = "behzad.b.i.g@gmail.com",
                Password = adminPassword,
                Role = "Admin"
            };

            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, adminPassword);
            modelBuilder.Entity<User>().HasData(user);
        }

        #endregion Creation 
    }

}
