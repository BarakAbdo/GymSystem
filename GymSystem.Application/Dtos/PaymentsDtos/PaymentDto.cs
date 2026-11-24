using System;

namespace GymSystem.Application.Dtos.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int SubscriptionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public string PaymentMethod { get; set; } = "Cash"; // Cash, Card, Transfer
        public string Status { get; set; } = "Paid"; // Paid, Pending, Cancelled
        public string? ReferenceNumber { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdatePaymentDto : CreatePaymentDto
    {
        public int Id { get; set; }
    }

    public class PaymentDto : UpdatePaymentDto
    {
        public string MemberName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
    }
}