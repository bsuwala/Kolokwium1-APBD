namespace ConsoleApplication1.Models
{

    using Microsoft.EntityFrameworkCore;

    namespace YourNamespace.Models
    {
        using Microsoft.EntityFrameworkCore;

        public class SubscriptionContext : DbContext
        {
            public DbSet<Client> Clients { get; set; }
            public DbSet<Subscription> Subscriptions { get; set; }
            public DbSet<Payment> Payments { get; set; }
            public DbSet<Discount> Discounts { get; set; }
            public DbSet<Sale> Sales { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=(localdb)\mssqllocaldb;Database=SubscriptionDB;Trusted_Connection=True;");
            }
        }
    }
}

