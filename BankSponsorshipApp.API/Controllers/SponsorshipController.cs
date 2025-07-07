using Microsoft.AspNetCore.Mvc;
using BankSponsorshipApp.Core.Services;
using BankSponsorshipApp.Data;
using BankSponsorshipApp.Models;

namespace BankSponsorshipApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SponsorshipController : ControllerBase
    {
        private readonly SponsorshipManager _service;

        public SponsorshipController(SponsorshipManager service)
        {
            _service = service;
        }

        [HttpGet("community-projects")]
        public ActionResult<List<CommunityProject>> GetCommunityProjects()
        {
            return _service.GetCommunityProjects();
        }

        [HttpPost("sponsorship-plan")]
        public IActionResult CreateSponsorshipPlan([FromBody] SponsorshipPlan plan)
        {
            _service.CreateSponsorshipPlan(plan);
            return Ok();
        }

        [HttpGet("sponsorship-plans/{customerId}")]
        public ActionResult<List<SponsorshipPlan>> GetSponsorshipPlans(int customerId)
        {
            return _service.GetSponsorshipPlansByCustomerId(customerId);
        }

        [HttpPost("process-payment")]
        public IActionResult ProcessPayment([FromBody] Payment payment)
        {
            _service.ProcessPayment(payment);
            return Ok();
        }

        [HttpGet("payments/{sponsorshipPlanId}")]
        public ActionResult<List<Payment>> GetPayments(int sponsorshipPlanId)
        {
            return _service.GetPaymentsBySponsorshipPlanId(sponsorshipPlanId);
        }
    }
}
