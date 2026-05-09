using JobFinder.Contracts.Enums;

namespace JobFinder.Contracts.Dtos.Account
{
    public class UserSettingsDto
    {
        public bool EmailNotifications { get; set; }
        public bool SmsNotifications { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public string Language { get; set; } = "en-US";
        public string TimeZone { get; set; } = "UTC";
        public bool ReceiveJobRecommendations { get; set; } = true;
        public NotificationPriority NotificationPriority { get; set; } = NotificationPriority.High;
        public string SavedSearchFilters { get; set; } = "";
        public bool PushNotifications { get; set; } = true;
        public bool IsProfilePublic { get; set; } = false;
        // Add other settings as needed
    }
}
