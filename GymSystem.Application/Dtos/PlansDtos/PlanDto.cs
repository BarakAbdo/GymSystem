using System;

namespace GymSystem.Application.Dtos.PlanDtos
{
    public class CreatePlanDto
    {
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public int DurationDays { get; set; }
        public decimal Price { get; set; }
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdatePlanDto : CreatePlanDto
    {
        public int Id { get; set; }
    }

    public class PlanDto : UpdatePlanDto
    {

    }
}