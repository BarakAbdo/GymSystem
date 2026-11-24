using System;

namespace GymSystem.Application.Dtos.SubscriptionsDtos
{
    public class CreateSubscriptionDto
    {
        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateSubscriptionDto : CreateSubscriptionDto
    {
        public int Id { get; set; }
    }

    public class SubscriptionDto : UpdateSubscriptionDto
    {
        public string MemberName { get; set; } = string.Empty;
        public string PlanName { get; set; } = string.Empty;
        public string PlanCategory { get; set; } = string.Empty; 
        public decimal Price { get; set; }
    }
}