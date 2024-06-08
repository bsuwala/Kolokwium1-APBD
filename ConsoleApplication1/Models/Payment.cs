using System;

namespace ConsoleApplication1.Models{

namespace YourNamespace.Models
{
    public class Payment
    {
        public int IdPayment { get; set; }
        public int IdClient { get; set; }
        public int IdSubscription { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public Client Client { get; set; }
        public Subscription Subscription { get; set; }
    }
}

}
