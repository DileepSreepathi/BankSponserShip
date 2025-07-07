using System.Collections.Generic;
using System.Linq;
using BankSponsorshipApp.Models;
using BankSponsorshipApp.Abstractions;
using Microsoft.Extensions.Logging;

namespace BankSponsorshipApp.Core.Services
{
    public class SponsorshipManager
    {
        private readonly ISponsorshipRepository _repository;
        private readonly ILogger<SponsorshipManager> _logger;

        public SponsorshipManager(ISponsorshipRepository repository, ILogger<SponsorshipManager> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public List<CommunityProject> GetCommunityProjects()
        {
            _logger.LogInformation("Fetching all community projects");
            return _repository.CommunityProjects;
        }

        public void CreateSponsorshipPlan(SponsorshipPlan plan)
        {
            _logger.LogInformation($"Creating sponsorship plan for customer {plan.CustomerId}");
            _repository.AddSponsorshipPlan(plan);
            // Add any payments included in the plan
            if (plan.Payments != null && plan.Payments.Count > 0)
            {
                foreach (var payment in plan.Payments)
                {
                    _repository.AddPayment(payment);
                }
            }
        }

        public List<SponsorshipPlan> GetSponsorshipPlansByCustomerId(int customerId)
        {
            _logger.LogInformation($"Fetching sponsorship plans for customer {customerId}");
            var plans = _repository.SponsorshipPlans.Where(sp => sp.CustomerId == customerId).ToList();
            foreach (var plan in plans)
            {
                plan.Payments = _repository.Payments.Where(p => p.SponsorshipPlanId == plan.Id).ToList();
            }
            return plans;
        }

        public void ProcessPayment(Payment payment)
        {
            _logger.LogInformation($"Processing payment for sponsorship plan {payment.SponsorshipPlanId}");
            _repository.AddPayment(payment);
        }

        public List<Payment> GetPaymentsBySponsorshipPlanId(int sponsorshipPlanId)
        {
            _logger.LogInformation($"Fetching payments for sponsorship plan {sponsorshipPlanId}");
            return _repository.GetPaymentsBySponsorshipPlanId(sponsorshipPlanId);
        }
    }
}
