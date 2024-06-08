using ConsoleApplication1.Models.YourNamespace.Models;

namespace ConsoleApplication1.Models{

using System.Collections.Generic;

public class Client
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<Payment> Payments { get; set; }
    public List<Sale> Sales { get; set; }
}
}