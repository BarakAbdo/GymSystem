using System;

namespace GymSystem.Domain.Entities
{
    public class Trainer
    {
        public int Id { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Specialty { get; set; } 
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
    }
}