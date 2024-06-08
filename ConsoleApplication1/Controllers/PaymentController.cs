using ConsoleApplication1.Models.YourNamespace.Models;

namespace ConsoleApplication1.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using ConsoleApplication1.Models;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using System;

    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly SubscriptionContext _context;

        public PaymentController(SubscriptionContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePayment(int idClient, int idSubscription, decimal paymentAmount)
        {
            
            var client = await _context.Clients.FindAsync(idClient);
            if (client == null)
            {
                return NotFound("Klient nie istnieje.");
            }

      
            var subscription = await _context.Subscriptions.FindAsync(idSubscription);
            if (subscription == null)
            {
                return NotFound("Subskrypcja nie istnieje.");
            }

        
            if (subscription.EndTime < DateTime.Now)
            {
                return BadRequest("Subskrypcja jest nieaktywna.");
            }

           
            var existingPayment = await _context.Payments
                .Where(p => p.IdClient == idClient && p.IdSubscription == idSubscription && p.Date > DateTime.Now.AddMonths(-subscription.RenewalPeriod))
                .FirstOrDefaultAsync();

            if (existingPayment != null)
            {
                return BadRequest("Subskrypcja została już opłacona na ten okres.");
            }

          
            decimal subscriptionPrice = subscription.Price;

        
            var activeDiscounts = await _context.Discounts
                .Where(d => d.IdSubscription == idSubscription && d.DateFrom <= DateTime.Now && d.DateTo >= DateTime.Now)
                .ToListAsync();

            if (activeDiscounts.Any())
            {
                var maxDiscount = activeDiscounts.Max(d => d.Value);
                subscriptionPrice = subscriptionPrice - (subscriptionPrice * maxDiscount / 100);
            }

            if (paymentAmount != subscriptionPrice)
            {
                return BadRequest("Wpłacana kwota nie jest zgodna z kwotą subskrypcji.");
            }

            
            var payment = new Payment
            {
                IdClient = idClient,
                IdSubscription = idSubscription,
                Amount = paymentAmount,
                Date = DateTime.Now
            };

            
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPayment), new { id = payment.IdPayment }, payment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
    }
}
