using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Domain.Common.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Persistance.DatabaseContext.ReadDbContext
{
    public class ReadDbContext : DbContext //,
    {
        private readonly IConfiguration _configuration;
        public ReadDbContext(DbContextOptions<ReadDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyFollow> CompanyFollows { get; set; }

        public DbSet<CompanyBenefit> CompanyBenefits { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

        public virtual DbSet<Education> Educations { get; set; }


        public virtual DbSet<FaqCategory> FaqCategory { get; set; }
        public virtual DbSet<FaqQuestion> FaqQuestion { get; set; }


        public virtual DbSet<Feature> Features { get; set; }


        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<SavedJob> SavedJobs { get; set; }
        public virtual DbSet<JobCategory> JobCategories { get; set; }

        public DbSet<JobPost> JobPosts { get; set; }

        public DbSet<JobRequest> JobRequests { get; set; }

        //public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<OfferDetails> OfferDetails { get; set; }
        public DbSet<RejectionDetails> RejectionDetails { get; set; }
        public DbSet<SubmissionDetails> SubmissionDetails { get; set; }
        public DbSet<InterviewDetail> InterviewDetails { get; set; }

        public DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Language> Languages { get; set; }


        public virtual DbSet<MBTIQuestion> MBTIQuestions { get; set; }
        public virtual DbSet<MBTIResult> MBTIResults { get; set; }

        public virtual DbSet<MBTIResultAnswer> MBTIResultAnswers { get; set; }
        public virtual DbSet<MenuItem> MenuItems { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


        public virtual DbSet<PricingCategory> PricingCategories { get; set; }
        public virtual DbSet<PricingFeature> PricingFeatures { get; set; }
        public virtual DbSet<PricingPlan> PricingPlans { get; set; }

        public DbSet<Logs> Logs { get; set; }


        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ProductInventory> ProductInventories { get; set; }
        public virtual DbSet<ProductInventorySnapshot> ProductInventorySnapshots { get; set; }

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
        public DbSet<PsychologyTestInterpretation> PsychologyTestInterpretations { get; set; }
        public DbSet<PsychologyTestResponseAnswer> PsychologyTestResponseAnswers { get; set; }
        public DbSet<JobTestAssignment> JobTestAssignments { get; set; }
        public DbSet<PersonalityTrait> PersonalityTraits { get; set; }
        public DbSet<PersonalityTestResult> PersonalityTestResults { get; set; }
        public DbSet<PersonalityTestResponse> PersonalityTestResponses { get; set; }
        public DbSet<PersonalityTestItem> PersonalityTestItems { get; set; }

        public DbSet<TermsOfService> TermsOfServices { get; set; }
        public DbSet<TermsSection> TermsSections { get; set; }

        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public virtual DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserSetting> UsersSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("AspNetUsers"); // Map to AspNetUsers
                entity.HasKey(u => u.Id); // Primary key (string from IdentityUser)

                // IdentityUser properties
                entity.Property(u => u.Id).HasColumnType("nvarchar(450)");
                entity.Property(u => u.UserName).HasMaxLength(256);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(256);
                entity.Property(u => u.Email).HasMaxLength(256).IsRequired(false);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(256);
                entity.Property(u => u.EmailConfirmed).IsRequired();
                entity.Property(u => u.PasswordHash).HasMaxLength(256);
                entity.Property(u => u.SecurityStamp).HasMaxLength(256);
                entity.Property(u => u.ConcurrencyStamp).HasMaxLength(256);
                entity.Property(u => u.PhoneNumber).HasMaxLength(50);
                entity.Property(u => u.PhoneNumberConfirmed).IsRequired();
                entity.Property(u => u.TwoFactorEnabled).IsRequired();
                entity.Property(u => u.LockoutEnd).HasColumnType("datetimeoffset");
                entity.Property(u => u.LockoutEnabled).IsRequired();
                entity.Property(u => u.AccessFailedCount).IsRequired();

                // Custom User properties
                entity.Property(u => u.FirstName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.LastName).HasMaxLength(100).IsRequired();
                entity.Property(u => u.Password).HasMaxLength(256).IsRequired();
                entity.Property(u => u.IsActive).IsRequired(false);
                entity.Property(u => u.PictureUrl).HasMaxLength(2048);
                entity.Property(u => u.Role).HasMaxLength(50).IsRequired();
                entity.Property(u => u.RefreshToken).HasMaxLength(256);
                entity.Property(u => u.RefreshTokenExpiryTime).HasColumnType("datetime2");
                entity.Property(u => u.DateCreated).HasColumnType("datetime2").IsRequired();
                entity.Property(u => u.DateModified).HasColumnType("datetime2");

                // Navigation properties

                // Ignore NotMapped property
                entity.Ignore(u => u.RedirectUrl);
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


            modelBuilder.Entity<PsychologyTestResponse>()
               .HasOne(x => x.PsychologyTest)
               .WithMany(pt => pt.PsychologyTestResponses) // Ensure the navigation property is correctly specified
               .HasForeignKey(x => x.PsychologyTestId) // Specify the foreign key explicitly
               .OnDelete(DeleteBehavior.NoAction); // Move OnDelete to the correct method chain




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

            modelBuilder.Entity<WorkExperience>()
         .HasOne(we => we.Resume)
         .WithMany(r => r.WorkExperiences)
         .HasForeignKey(we => we.ResumeId)
         .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MenuItem>()
                .HasOne(m => m.Parent)
                .WithMany(m => m.Children)
                .HasForeignKey(m => m.ParentId);


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

            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd(); // Mark Id as database-generated
            });
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


            // Configure other entities with identity columns similarly
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Advertisement>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CandidateJobPreferences>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Company>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CompanyBenefit>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CompanyJobPreferences>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<CustomerAddress>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Education>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<FaqCategory>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<FaqQuestion>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Feature>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<InterviewDetail>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();

                entity.Property(e => e.InterviewId)
                  .ValueGeneratedNever();

            });
            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<JobApplication>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<JobCategory>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<JobPost>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<JobRequest>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<JobTestAssignment>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Language>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<MBTIQuestion>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<MBTIResult>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<OfferDetails>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PersonalityTestItem>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PersonalityTestResponse>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PersonalityTestResult>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PersonalityTrait>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PricingCategory>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });

            //modelBuilder.Entity<PricingFeature>()
            // .HasOne(pf => pf.PricingPlan)
            // .WithMany()
            // .HasForeignKey(pf => pf.PricingPlanId)
            // .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PricingPlan>()
                .Property(p => p.Title)
                .IsRequired();

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<ProductInventorySnapshot>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Province>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PsychologyTest>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PsychologyTestQuestion>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PsychologyTestResponse>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<PsychologyTestResult>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<RejectionDetails>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Resume>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SalesOrder>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SalesOrderItem>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<Skill>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<SubmissionDetails>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TechnicalOption>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TermsOfService>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<TermsSection>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<WorkExperience>(entity =>
            {
                entity.Property(e => e.Id)
                      .ValueGeneratedOnAdd();
            });
            //AddSpecialUser(modelBuilder);

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


            base.OnModelCreating(modelBuilder);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(w => w.Log(CoreEventId.InvalidIncludePathError));
        }


        private void AddSpecialUser(ModelBuilder modelBuilder)
        {
            string adminPassword = _configuration["ADMINPASS:PASSWORD"];
            if (string.IsNullOrEmpty(adminPassword))
            {
                throw new Exception("Admin password is missing from configuration.", new Exception("adminPassword Need To Be Added"));
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


    }

}
