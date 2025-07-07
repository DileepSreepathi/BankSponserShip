using System.Collections.Generic;
using BankSponsorshipApp.Models;

namespace BankSponsorshipApp.Abstractions
{
    public interface ISponsorshipRepository
    {
        List<Customer> Customers { get; }
        List<CommunityProject> CommunityProjects { get; }
        List<SponsorshipPlan> SponsorshipPlans { get; }
        List<Payment> Payments { get; }
        CommunityProject? GetCommunityProjectById(int id);
        Customer? GetCustomerById(int id);
        void AddSponsorshipPlan(SponsorshipPlan plan);
        void AddPayment(Payment payment);
        List<Payment> GetPaymentsBySponsorshipPlanId(int sponsorshipPlanId);
    }
}
