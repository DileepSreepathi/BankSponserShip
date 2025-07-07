using Xunit;
using BankSponsorshipApp.Data.Repositories;
using BankSponsorshipApp.Models;

namespace BankSponsorshipApp.Tests
{
    public class InMemorySponsorshipRepositoryTests
    {
        [Fact]
        public void GetCommunityProjectById_ReturnsCorrectProject()
        {
            var repo = new InMemorySponsorshipRepository();
            var project = repo.GetCommunityProjectById(1);
            Assert.NotNull(project);
            Assert.Equal("Municipality welfare funds", project?.Name);
        }

        [Fact]
        public void AddSponsorshipPlan_StoresPlan()
        {
            var repo = new InMemorySponsorshipRepository();
            var plan = new SponsorshipPlan { Id = 99, CustomerId = 1, CommunityProjectId = 1, Amount = 50, Frequency = "Once-off" };
            repo.AddSponsorshipPlan(plan);
            Assert.Contains(repo.SponsorshipPlans, p => p.Id == 99);
        }
    }
}
