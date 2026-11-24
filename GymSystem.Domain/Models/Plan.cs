using System;
using System.Collections.Generic;

namespace GymSystem.Domain.Models
{
    public class Plan
    {
        public int Id { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }

        // علاقة واحد لمتعدد (الباقة الواحدة يمكن أن يشترك فيها أكثر من عضو)
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}