using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Persistance.DatabaseContext.WriteDbContext;
using Persistance.Interfaces;

namespace Persistance.Interceptors
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IDateTimeProvider _dateTimeProvider; // Custom service for consistent time

        public AuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor, IDateTimeProvider dateTimeProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _dateTimeProvider = dateTimeProvider;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context is WriteDbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries();

                foreach (var entry in entries)
                {
                    if (entry.Entity is IAuditableEntity auditableEntity)
                    {
                        var now = _dateTimeProvider.Now;
                        var userId = GetCurrentUserId(); // Implement logic to get current user ID
                        var ipAddress = GetUserIPAddress();
                        if (entry.State == EntityState.Added)
                        {
                            auditableEntity.CreatedDate = now;
                            auditableEntity.CreatedBy = userId;
                            auditableEntity.IpAddress = ipAddress;
                        }
                        else if (entry.State == EntityState.Modified)
                        {
                            auditableEntity.LastModifiedDate = now;
                            auditableEntity.LastModifiedBy = userId;
                            auditableEntity.IpAddress = ipAddress;
                        }
                    }
                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private string GetCurrentUserId()
        {
            // Example using HttpContextAccessor and Identity
            var httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated == true)
            {
                return httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier); // Or your user ID claim
            }
            return null;
        }


        private string GetUserIPAddress()
        {
            var ipAddress = _httpContextAccessor?.HttpContext?.Connection.RemoteIpAddress?.ToString();
            return ipAddress;
        }
    }

}
