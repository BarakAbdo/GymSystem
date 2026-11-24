using GymSystem.Application.Dtos.TrainerDtos;
using System.Collections.Generic;

namespace GymSystem.Application.Services
{
    public interface ITrainerService
    {
        IEnumerable<TrainerDto> GetAll();
        TrainerDto? GetById(int id);
        TrainerDto? GetByUid(string uid);
        void Add(CreateTrainerDto dto);
        void Update(string uid, UpdateTrainerDto dto);
        void Delete(string uid);
    }
}