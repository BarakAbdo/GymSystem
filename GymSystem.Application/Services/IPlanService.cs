
using GymSystem.Application.Dtos.PlanDtos;
using System.Collections.Generic;

namespace GymSystem.Application.Services.PlanService
{
    public interface IPlanService
    {
        IEnumerable<PlanDto> GetAll();
        PlanDto? GetById(int id);
        PlanDto? GetByUid(string uid);
        void Add(CreatePlanDto dto);
        void Update(string uid, UpdatePlanDto dto);
        void Delete(string uid);
    }
}