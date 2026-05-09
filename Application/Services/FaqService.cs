using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobFinder.Application.Common.Interfaces.Services;
using JobFinder.Domain.Common.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace JobFinder.Application.Services
{
    public class FaqService : IFaqService
    {
        private readonly IMemoryCache _cache;
        private const string EmployerFaqCacheKey = "EmployerFaqs";

        public FaqService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<FaqCategory>> GetEmployerFaqsAsync()
        {
            // Try to get from cache first
            if (_cache.TryGetValue(EmployerFaqCacheKey, out IEnumerable<FaqCategory> cachedFaqs))
            {
                return cachedFaqs;
            }

            // If not in cache, get from data source (in this case, hardcoded data)
            var faqs = GetEmployerFaqData();

            // Cache the data for 1 hour
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromHours(1));

            _cache.Set(EmployerFaqCacheKey, faqs, cacheOptions);

            return faqs;
        }

        private IEnumerable<FaqCategory> GetEmployerFaqData()
        {
            // This would typically come from a database, but for this example, we'll hardcode it
            return new List<FaqCategory>
            {
                new FaqCategory
                {
                    Id = 1,
                    Name = "Account Setup",
                    Questions = new List<FaqQuestion>
                    {
                        new FaqQuestion
                        {
                            Id = 1,
                            Question = "How do I create an employer account?",
                            Answer = "<p>To create an employer account:</p><ol><li>Click on the 'Employers' button in the top navigation</li><li>Select 'Register as Employer'</li><li>Fill out the required information including company details</li><li>Verify your email address</li><li>Complete your company profile</li></ol>"
                        },
                        new FaqQuestion
                        {
                            Id = 2,
                            Question = "What information do I need to provide when creating an account?",
                            Answer = "<p>When creating an employer account, you'll need to provide:</p><ul><li>Company name</li><li>Industry</li><li>Company size</li><li>Company location</li><li>Contact person details</li><li>Company email domain</li><li>Company website</li></ul><p>Additional documents may be required for verification purposes.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 3,
                            Question = "How long does account verification take?",
                            Answer = "<p>Account verification typically takes 1-2 business days. For larger companies or those requiring additional verification, it may take up to 3-5 business days. You'll receive an email notification once your account has been verified.</p>"
                        }
                    }
                },
                new FaqCategory
                {
                    Id = 2,
                    Name = "Job Postings",
                    Questions = new List<FaqQuestion>
                    {
                        new FaqQuestion
                        {
                            Id = 4,
                            Question = "How do I post a job?",
                            Answer = "<p>To post a job:</p><ol><li>Log in to your employer account</li><li>Click on 'Post a Job' button in your dashboard</li><li>Fill out the job details form including title, description, requirements, and benefits</li><li>Select the appropriate job category and location</li><li>Choose your posting package</li><li>Review and publish your job</li></ol>"
                        },
                        new FaqQuestion
                        {
                            Id = 5,
                            Question = "What are the costs for posting jobs?",
                            Answer = "<p>We offer several pricing packages:</p><ul><li><strong>Basic:</strong> $99 per job post for 30 days</li><li><strong>Standard:</strong> $249 for 3 job posts valid for 60 days</li><li><strong>Premium:</strong> $499 for 10 job posts valid for 90 days</li><li><strong>Enterprise:</strong> Custom pricing for unlimited job posts</li></ul><p>All packages include applicant tracking and basic analytics.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 6,
                            Question = "How can I edit a job posting?",
                            Answer = "<p>To edit an active job posting:</p><ol><li>Log in to your employer dashboard</li><li>Go to 'My Job Postings'</li><li>Find the job you want to edit and click the 'Edit' button</li><li>Make your changes</li><li>Click 'Save Changes'</li></ol><p>Note: Major changes to job requirements or compensation may require a new job posting.</p>"
                        }
                    }
                },
                new FaqCategory
                {
                    Id = 3,
                    Name = "Candidate Management",
                    Questions = new List<FaqQuestion>
                    {
                        new FaqQuestion
                        {
                            Id = 7,
                            Question = "How do I view applicants for my job posting?",
                            Answer = "<p>To view applicants:</p><ol><li>Log in to your employer dashboard</li><li>Go to 'My Job Postings'</li><li>Click on the job title or the 'View Applicants' button</li><li>You'll see a list of all applicants with their key information</li><li>Click on an applicant's name to view their full profile and resume</li></ol>"
                        },
                        new FaqQuestion
                        {
                            Id = 8,
                            Question = "Can I download resumes of applicants?",
                            Answer = "<p>Yes, you can download resumes of applicants in several ways:</p><ul><li>Individual resume: Open the applicant's profile and click 'Download Resume'</li><li>Bulk download: From the applicants list, select multiple candidates and click 'Download Selected Resumes'</li><li>All resumes: From the applicants list, click 'Download All Resumes'</li></ul><p>Resumes are available in PDF format.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 9,
                            Question = "How do I contact candidates?",
                            Answer = "<p>You can contact candidates through our platform in several ways:</p><ul><li>Direct message: Click 'Message' on the candidate's profile</li><li>Bulk message: Select multiple candidates and click 'Message Selected'</li><li>Interview request: Click 'Schedule Interview' on the candidate's profile</li><li>Status update: Change the candidate's status to notify them of their application progress</li></ul><p>All communications are logged in your account for future reference.</p>"
                        }
                    }
                },
                new FaqCategory
                {
                    Id = 4,
                    Name = "Billing and Subscriptions",
                    Questions = new List<FaqQuestion>
                    {
                        new FaqQuestion
                        {
                            Id = 10,
                            Question = "How do I upgrade my subscription?",
                            Answer = "<p>To upgrade your subscription:</p><ol><li>Log in to your employer dashboard</li><li>Go to 'Account Settings' > 'Subscription'</li><li>Click 'Upgrade Plan'</li><li>Select your desired plan</li><li>Complete the payment process</li><li>Your new plan benefits will be activated immediately</li></ol>"
                        },
                        new FaqQuestion
                        {
                            Id = 11,
                            Question = "What payment methods do you accept?",
                            Answer = "<p>We accept the following payment methods:</p><ul><li>Credit/Debit Cards (Visa, MasterCard, American Express)</li><li>PayPal</li><li>Bank Transfer (for annual subscriptions only)</li><li>Purchase Orders (for enterprise customers)</li></ul><p>All payments are processed securely through our payment gateway.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 12,
                            Question = "How do I get an invoice for my payment?",
                            Answer = "<p>Invoices are automatically generated and sent to your registered email address after each payment. To access your invoices:</p><ol><li>Log in to your employer dashboard</li><li>Go to 'Account Settings' > 'Billing History'</li><li>Find the payment and click 'View Invoice'</li><li>You can download the invoice as a PDF or print it directly</li></ol><p>For special invoice requirements, please contact our billing department.</p>"
                        }
                    }
                },
                new FaqCategory
                {
                    Id = 5,
                    Name = "Account Security",
                    Questions = new List<FaqQuestion>
                    {
                        new FaqQuestion
                        {
                            Id = 13,
                            Question = "How do I reset my password?",
                            Answer = "<p>To reset your password:</p><ol><li>Click on 'Login' in the top navigation</li><li>Click 'Forgot Password'</li><li>Enter your registered email address</li><li>Check your email for a password reset link</li><li>Click the link and create a new password</li></ol><p>The password reset link is valid for 24 hours.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 14,
                            Question = "How do I add team members to my account?",
                            Answer = "<p>To add team members to your employer account:</p><ol><li>Log in to your employer dashboard</li><li>Go to 'Account Settings' > 'Team Management'</li><li>Click 'Add Team Member'</li><li>Enter their email address and select their role (Admin, Recruiter, Viewer)</li><li>Click 'Send Invitation'</li></ol><p>The team member will receive an email invitation to join your account.</p>"
                        },
                        new FaqQuestion
                        {
                            Id = 15,
                            Question = "What are the different user roles and permissions?",
                            Answer = "<p>We offer three different user roles with varying permissions:</p><ul><li><strong>Admin:</strong> Full access to all features, can manage billing, add/remove team members, and modify company profile</li><li><strong>Recruiter:</strong> Can post jobs, view and contact candidates, but cannot access billing or add team members</li><li><strong>Viewer:</strong> Can only view job postings and candidates, but cannot make changes or contact candidates</li></ul><p>Custom roles with specific permissions can be set up for Enterprise accounts.</p>"
                        }
                    }
                }
            };
        }
    }
}
