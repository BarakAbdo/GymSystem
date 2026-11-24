using GymSystem.Application.Dtos.PlanDtos;
using GymSystem.Application.Services.PlanService;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;


namespace GymSystem.Application.Services.PlanService
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanDto> GetAll()
        {
            var plans = _unitOfWork.PlanRepo.GetAll();

            return plans.Select(p => new PlanDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Category = p.Category,
                DurationDays = p.DurationDays,
                Price = p.Price
            }).ToList();
        }

        public PlanDto? GetById(int id)
        {
            var p = _unitOfWork.PlanRepo.GetById(id);
            if (p == null) return null;

            return new PlanDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Category = p.Category,
                DurationDays = p.DurationDays,
                Price = p.Price
            };
        }

        public PlanDto? GetByUid(string uid)
        {
            var p = _unitOfWork.PlanRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (p == null) return null;

            return new PlanDto
            {
                Id = p.Id,
                UID = p.UID,
                Name = p.Name,
                Category = p.Category,
                DurationDays = p.DurationDays,
                Price = p.Price
            };
        }

        public void Add(CreatePlanDto dto)
        {
            var plan = new Plan
            {
                UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
                Name = dto.Name,
                Category = dto.Category,
                DurationDays = dto.DurationDays,
                Price = dto.Price
            };

            _unitOfWork.PlanRepo.Add(plan);
            _unitOfWork.Save();
        }

        public void Update(string uid, UpdatePlanDto dto)
        {
            var plan = _unitOfWork.PlanRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (plan == null) return;

            plan.Name = dto.Name;
            plan.Category = dto.Category;
            plan.DurationDays = dto.DurationDays;
            plan.Price = dto.Price;

            _unitOfWork.PlanRepo.Update(plan);
            _unitOfWork.Save();
        }

        public void Delete(string uid)
        {
            var plan = _unitOfWork.PlanRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (plan == null) return;

            _unitOfWork.PlanRepo.Delete(plan);
            _unitOfWork.Save();
        }
    }
}