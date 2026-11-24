using System.Collections.Generic;
using GymSystem.Application.Dtos.PaymentDtos;

namespace GymSystem.Application.Services
{
    public interface IPaymentService
    {
        IEnumerable<PaymentDto> GetAll();
        PaymentDto? GetById(int id);
        PaymentDto? GetByUid(string uid);
        void Add(CreatePaymentDto dto);
        void Update(string uid, UpdatePaymentDto dto);
        void Delete(string uid);
    }
}