using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymSystem.Domain.Models
{
    public class Subscription
    {
        public int Id { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey(nameof(Member))]
        public int MemberId { get; set; }
        public Member? Member { get; set; }


        [ForeignKey(nameof(Plan))]
        public int PlanId { get; set; }
        public Plan? Plan { get; set; }
    }
}