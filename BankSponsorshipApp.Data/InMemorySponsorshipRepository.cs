using System.Collections.Generic;
using System.Linq;
using BankSponsorshipApp.Models;
using BankSponsorshipApp.Abstractions;

namespace BankSponsorshipApp.Data.Repositories
{
    public class InMemorySponsorshipRepository : ISponsorshipRepository
    {
        public List<Customer> Customers { get; set; } = new List<Customer>();
        public List<CommunityProject> CommunityProjects { get; set; } = new List<CommunityProject>();
        public List<SponsorshipPlan> SponsorshipPlans { get; set; } = new List<SponsorshipPlan>();
        public List<Payment> Payments { get; set; } = new List<Payment>();

        public InMemorySponsorshipRepository()
        {
            // Seed some sample data
            CommunityProjects.Add(new CommunityProject
            {
                Id = 1,
                Name = "Municipality welfare funds",
                StartDate = DateTime.Now.AddMonths(-2),
                EndDate = DateTime.Now.AddMonths(2),
                TotalFundsRequired = 10000,
                TotalFundsRaised = 2500
            });

            CommunityProjects.Add(new CommunityProject
            {
                Id = 2,
                Name = "Education for All",
                StartDate = DateTime.Now.AddMonths(-1),
                EndDate = DateTime.Now.AddMonths(3),
                TotalFundsRequired = 20000,
                TotalFundsRaised = 5000
            });
        }

        public CommunityProject? GetCommunityProjectById(int id)
        {
            return CommunityProjects.FirstOrDefault(cp => cp.Id == id);
        }

        public Customer? GetCustomerById(int id)
        {
            return Customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddSponsorshipPlan(SponsorshipPlan plan)
        {
            if (!SponsorshipPlans.Any(p => p.Id == plan.Id))
            {
                SponsorshipPlans.Add(plan);
            }
        }

        public void AddPayment(Payment payment)
        {
            Payments.Add(payment);
            var plan = SponsorshipPlans.FirstOrDefault(sp => sp.Id == payment.SponsorshipPlanId);
            if (plan != null)
            {
                plan.Payments.Add(payment);
            }
        }

        public List<Payment> GetPaymentsBySponsorshipPlanId(int sponsorshipPlanId)
        {
            return Payments.Where(p => p.SponsorshipPlanId == sponsorshipPlanId).ToList();
        }
    }
}
