using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobFinder.Contracts.Dtos.PushSubscription
{
    public class PushSubscriptionDto
    {
        public string Endpoint { get; set; }
        public PushSubscriptionKeys Keys { get; set; }
    }
    public class PushSubscriptionKeys
    {
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }

    public class NotificationRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
