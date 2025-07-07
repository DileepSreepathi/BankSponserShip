using System.Collections.Generic;
using System.Linq;
using BankSponsorshipApp.Models;

namespace BankSponsorshipApp.Core.Services
{
    public interface IDataStore
    {
        List<Customer> Customers { get; set; }
        List<CommunityProject> CommunityProjects { get; set; }
        List<SponsorshipPlan> SponsorshipPlans { get; set; }
        List<Payment> Payments { get; set; }
        void AddSponsorshipPlan(SponsorshipPlan plan);
        void AddPayment(Payment payment);
        List<Payment> GetPaymentsBySponsorshipPlanId(int sponsorshipPlanId);
    }

    public class SponsorshipService
    {
        private readonly IDataStore _dataStore;

        public SponsorshipService(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public List<CommunityProject> GetCommunityProjects()
        {
            return _dataStore.CommunityProjects;
        }

        public void CreateSponsorshipPlan(SponsorshipPlan plan)
        {
            _dataStore.AddSponsorshipPlan(plan);
        }

        public List<SponsorshipPlan> GetSponsorshipPlansByCustomerId(int customerId)
        {
            return _dataStore.SponsorshipPlans.Where(sp => sp.CustomerId == customerId).ToList();
        }

        public void ProcessPayment(Payment payment)
        {
            _dataStore.AddPayment(payment);
        }

        public List<Payment> GetPaymentsBySponsorshipPlanId(int sponsorshipPlanId)
        {
            return _dataStore.GetPaymentsBySponsorshipPlanId(sponsorshipPlanId);
        }
    }
}
