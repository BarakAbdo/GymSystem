using GymSystem.Domain.Models;

namespace GymSystem.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; } = null!;

        public string UID { get; set; } = Guid.NewGuid().ToString();

        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Card, Transfer
        public string Status { get; set; } = "Paid"; // Paid, Pending, Cancelled
        public string? ReferenceNumber { get; set; }
        public string? Notes { get; set; }
    }
}