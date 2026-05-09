using System.Text.Json;
using Lib.Net.Http.WebPush;
using Lib.Net.Http.WebPush.Authentication;
using Microsoft.EntityFrameworkCore;
using Persistance.DatabaseContext.WriteDbContext;

namespace JobFinder.Services
{
    public class PushNotificationService
    {
        private readonly WriteDbContext _context;
        private readonly PushServiceClient _pushClient;
        private const string PublicKey = "BKNbFZij6XgGCsoqenEHK87QSFu84ZBf6ZscJWMcHm3lOEU5gixg3_Nj6viE-5BglCN5kYDXj6Xs178peYrAU58";
        private const string PrivateKey = "N6eIEon4ta9jTR9o-9xo7akr5Svf8uieMuJZ-G0UvW8";

        public PushNotificationService(WriteDbContext context)
        {
            _context = context;

            _pushClient = new PushServiceClient
            {
                DefaultAuthentication = new VapidAuthentication(publicKey: PublicKey, privateKey: PrivateKey)
                {
                    Subject = "mailto:your@email.com",
                    PublicKey = PublicKey,
                    PrivateKey = PrivateKey
                }
            };
        }

        public async Task SendNotificationAsync(string title, string body)
        {
            var subscriptions = await _context.PushSubscriptions.ToListAsync();
            foreach (var sub in subscriptions)
            {
                var dic = new Dictionary<string, string>();
                dic.Add(sub.P256DH, sub.Auth);

                var pushSubscription = new PushSubscription
                {
                    Endpoint = sub.Endpoint,
                    Keys = dic,
                    //new PushSubscriptionKeys
                    //{
                    //    P256DH = sub.P256DH,
                    //    Auth = sub.Auth
                    //}
                };

                var payload = JsonSerializer.Serialize(new { title, body });
                PushMessage pushMessage = new PushMessage(body)
                {
                    TimeToLive = 3000,
                    Topic = title,
                    Urgency = PushMessageUrgency.High,
                };
                try
                {
                    await _pushClient.RequestPushMessageDeliveryAsync(pushSubscription, pushMessage);
                }
                catch (Exception ex)
                {
                    // Log and remove expired subscription
                }
            }
        }
    }

}
