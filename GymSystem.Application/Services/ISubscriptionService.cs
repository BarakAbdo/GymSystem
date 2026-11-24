using GymSystem.Application.Dtos.SubscriptionsDtos;
using System.Collections.Generic;

namespace GymSystem.Application.Services.SubscriptionService
{
    public interface ISubscriptionService
    {
        IEnumerable<SubscriptionDto> GetAll();
        SubscriptionDto? GetById(int id);
        SubscriptionDto? GetByUid(string uid);
        void Add(CreateSubscriptionDto dto);
        void Update(string uid, UpdateSubscriptionDto dto);
        void Delete(string uid);
    }
}