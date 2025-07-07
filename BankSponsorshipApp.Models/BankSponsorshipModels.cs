namespace BankSponsorshipApp.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }

    public class CommunityProject
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalFundsRequired { get; set; }
        public decimal TotalFundsRaised { get; set; }
    }

    public class SponsorshipPlan
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int CommunityProjectId { get; set; }
        public string? SourceOfFunds { get; set; } // Account or Card
        public string? SourceType { get; set; } // Type of source (e.g., Account, Card)
        public decimal Amount { get; set; }
        public string? Frequency { get; set; } // Once-off, Weekly, Monthly
        public DateTime? StartDate { get; set; }
        public List<Payment> Payments { get; set; } = new List<Payment>();
        public bool IsPaid => Payments.Any(p => p.Amount >= Amount); // Simple paid check
    }

    public class Payment
    {
        public int Id { get; set; }
        public int SponsorshipPlanId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string Status { get; set; } = "Processed";
    }
}
