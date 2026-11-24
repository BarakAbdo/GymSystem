using System;
using System.Collections.Generic;

namespace GymSystem.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        // علاقة واحد لمتعدد (العضو الواحد يمكن يكون عنده أكثر من اشتراك)
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
    }
}