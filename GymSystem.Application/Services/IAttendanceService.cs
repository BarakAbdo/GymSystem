using System.Collections.Generic;
using GymSystem.Application.Dtos.AttendanceDtos;

namespace GymSystem.Application.Services
{
    public interface IAttendanceService
    {
        IEnumerable<AttendanceDto> GetAll();
        AttendanceDto? GetById(int id);
        AttendanceDto? GetByUid(string uid);
        void Add(CreateAttendanceDto dto);
        void Update(string uid, UpdateAttendanceDto dto);
        void Delete(string uid);
    }
}