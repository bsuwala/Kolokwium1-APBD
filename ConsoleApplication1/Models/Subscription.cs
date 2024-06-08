using System;
using System.Collections.Generic;
using ConsoleApplication1.Models.YourNamespace.Models;

namespace ConsoleApplication1.Models{

public class Subscription
{
    public int IdSubscription { get; set; }
    public string Name { get; set; }
    public int RenewalPeriod { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }

    public List<Payment> Payments { get; set; }
    public List<Sale> Sales { get; set; }
    public List<Discount> Discounts { get; set; }
}
}