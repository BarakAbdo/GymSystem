using System.Collections.Generic;
using System.Linq;
using GymSystem.Application.Dtos.TrainerDtos;
using GymSystem.Application.Services;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Services
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<TrainerDto> GetAll()
        {
            var trainers = _unitOfWork.TrainerRepo.GetAll();

            return trainers.Select(t => new TrainerDto
            {
                Id = t.Id,
                UID = t.UID,
                Name = t.Name,
                Specialty = t.Specialty,
                Phone = t.Phone,
                Email = t.Email,
                Salary = t.Salary,
                IsActive = t.IsActive
            }).OrderBy(t => t.Name).ToList();
        }

        public TrainerDto? GetById(int id)
        {
            var t = _unitOfWork.TrainerRepo.GetById(id);
            if (t == null) return null;

            return new TrainerDto
            {
                Id = t.Id,
                UID = t.UID,
                Name = t.Name,
                Specialty = t.Specialty,
                Phone = t.Phone,
                Email = t.Email,
                Salary = t.Salary,
                IsActive = t.IsActive
            };
        }

        public TrainerDto? GetByUid(string uid)
        {
            var t = _unitOfWork.TrainerRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (t == null) return null;

            return new TrainerDto
            {
                Id = t.Id,
                UID = t.UID,
                Name = t.Name,
                Specialty = t.Specialty,
                Phone = t.Phone,
                Email = t.Email,
                Salary = t.Salary,
                IsActive = t.IsActive
            };
        }

        public void Add(CreateTrainerDto dto)
        {
            var trainer = new Trainer
            {
                UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
                Name = dto.Name,
                Specialty = dto.Specialty,
                Phone = dto.Phone,
                Email = dto.Email,
                Salary = dto.Salary,
                IsActive = dto.IsActive
            };

            _unitOfWork.TrainerRepo.Add(trainer);
            _unitOfWork.Save();
        }

        public void Update(string uid, UpdateTrainerDto dto)
        {
            var trainer = _unitOfWork.TrainerRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (trainer == null) return;

            trainer.Name = dto.Name;
            trainer.Specialty = dto.Specialty;
            trainer.Phone = dto.Phone;
            trainer.Email = dto.Email;
            trainer.Salary = dto.Salary;
            trainer.IsActive = dto.IsActive;

            _unitOfWork.TrainerRepo.Update(trainer);
            _unitOfWork.Save();
        }

        public void Delete(string uid)
        {
            var trainer = _unitOfWork.TrainerRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (trainer == null) return;

            _unitOfWork.TrainerRepo.Delete(trainer);
            _unitOfWork.Save();
        }
    }
}