using System;
using JobFinder.Contracts.Enums;
using JobFinder.Domain.Common;

namespace JobFinder.Domain.Common.Entities
{
    public class UserSetting : IBaseEntity<Guid>
    {
        public Guid Id { get; set; }
        public required string UserId { get; set; }
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Language { get; set; } = "ir-fa";
        public string TimeZone { get; set; } = "UTC";
        public bool ReceiveJobRecommendations { get; set; } = true;
        public NotificationPriority NotificationPriority { get; set; } = NotificationPriority.High;
        public string SavedSearchFilters { get; set; } = "";
        public bool PushNotifications { get; set; } = true;
        public bool IsProfilePublic { get; set; } = false;
        // Navigation property
        public virtual User? User { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateModified { get; set; }
    }


    
}
