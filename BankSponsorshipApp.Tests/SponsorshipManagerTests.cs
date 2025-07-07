using Xunit;
using BankSponsorshipApp.Core.Services;
using BankSponsorshipApp.Data.Repositories;
using BankSponsorshipApp.Models;
using BankSponsorshipApp.Abstractions;
using Microsoft.Extensions.Logging.Abstractions;
using System.Linq;

namespace BankSponsorshipApp.Tests
{
    public class SponsorshipManagerTests
    {
        private SponsorshipManager GetManagerWithSeededRepo()
        {
            ISponsorshipRepository repo = new InMemorySponsorshipRepository();
            var logger = NullLogger<SponsorshipManager>.Instance;
            return new SponsorshipManager(repo, logger);
        }

        [Fact]
        public void GetCommunityProjects_ReturnsSeededProjects()
        {
            var manager = GetManagerWithSeededRepo();
            var projects = manager.GetCommunityProjects();
            Assert.True(projects.Count >= 2);
            Assert.Contains(projects, p => p.Name == "Municipality welfare funds");
        }

        [Fact]
        public void CreateSponsorshipPlan_AddsPlan()
        {
            var manager = GetManagerWithSeededRepo();
            var plan = new SponsorshipPlan { Id = 1, CustomerId = 1, CommunityProjectId = 1, Amount = 100, Frequency = "Once-off" };
            manager.CreateSponsorshipPlan(plan);
            var plans = manager.GetSponsorshipPlansByCustomerId(1);
            Assert.Contains(plans, p => p.Id == 1);
        }

        [Fact]
        public void ProcessPayment_UpdatesPlanStatus()
        {
            var manager = GetManagerWithSeededRepo();
            var plan = new SponsorshipPlan { Id = 2, CustomerId = 2, CommunityProjectId = 2, Amount = 200, Frequency = "Monthly" };
            manager.CreateSponsorshipPlan(plan);
            var payment = new Payment { Id = 1, SponsorshipPlanId = 2, Amount = 200, PaymentDate = DateTime.Now };
            manager.ProcessPayment(payment);
            var updatedPlan = manager.GetSponsorshipPlansByCustomerId(2).FirstOrDefault(p => p.Id == 2);
            Assert.NotNull(updatedPlan);
            Assert.Single(updatedPlan.Payments);
            Assert.True(updatedPlan.IsPaid);
        }
    }
}
