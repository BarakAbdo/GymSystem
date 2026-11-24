using System;

namespace GymSystem.Application.Dtos.TrainerDtos
{
    public class CreateTrainerDto
    {
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty; 
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public bool IsActive { get; set; } = true;
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateTrainerDto : CreateTrainerDto
    {
        public int Id { get; set; }
    }

    public class TrainerDto : UpdateTrainerDto
    {

    }
}