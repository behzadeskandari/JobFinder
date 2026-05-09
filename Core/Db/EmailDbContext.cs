using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Db
{
    public class EmailDbContext : DbContext
    {
        public EmailDbContext(DbContextOptions<EmailDbContext> options) : base(options)
        {
        }

        public DbSet<EmailLog> EmailLogs { get; set; }
        public DbSet<SMSLog> SMSLogs { get; set; }
        public DbSet<SendResult> SendResults { get; set; }
        public DbSet<EmailMessage> EmailMessages { get; set; }

        public DbSet<GeneratedLink> GeneratedLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<GeneratedLink>().HasIndex(l => l.Token).IsUnique();

            // Configure your entities here
            base.OnModelCreating(modelBuilder);
        }
    }
}
