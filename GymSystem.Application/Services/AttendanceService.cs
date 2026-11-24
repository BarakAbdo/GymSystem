using System.Collections.Generic;
using System.Linq;
using GymSystem.Application.Dtos.AttendanceDtos;
using GymSystem.Domain.Entities;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendanceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<AttendanceDto> GetAll()
        {
            var attendances = _unitOfWork.AttendanceRepo.GetAll();
            var members = _unitOfWork.MemberRepo.GetAll().ToDictionary(m => m.Id);

            return attendances.Select(a =>
            {
                string memberName = members.ContainsKey(a.MemberId) ? members[a.MemberId].Name : "Unknown";

                return new AttendanceDto
                {
                    Id = a.Id,
                    UID = a.UID,
                    MemberId = a.MemberId,
                    MemberName = memberName,
                    CheckInTime = a.CheckInTime,
                    CheckOutTime = a.CheckOutTime,
                    Status = a.Status,
                    Notes = a.Notes
                };
            }).OrderByDescending(a => a.CheckInTime).ToList();
        }

        public AttendanceDto? GetById(int id)
        {
            var a = _unitOfWork.AttendanceRepo.GetById(id);
            if (a == null) return null;

            var member = _unitOfWork.MemberRepo.GetById(a.MemberId);

            return new AttendanceDto
            {
                Id = a.Id,
                UID = a.UID,
                MemberId = a.MemberId,
                MemberName = member?.Name ?? "Unknown",
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                Status = a.Status,
                Notes = a.Notes
            };
        }

        public AttendanceDto? GetByUid(string uid)
        {
            var a = _unitOfWork.AttendanceRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (a == null) return null;

            var member = _unitOfWork.MemberRepo.GetById(a.MemberId);

            return new AttendanceDto
            {
                Id = a.Id,
                UID = a.UID,
                MemberId = a.MemberId,
                MemberName = member?.Name ?? "Unknown",
                CheckInTime = a.CheckInTime,
                CheckOutTime = a.CheckOutTime,
                Status = a.Status,
                Notes = a.Notes
            };
        }

        public void Add(CreateAttendanceDto dto)
        {
            var attendance = new Attendance
            {
                UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
                MemberId = dto.MemberId,
                CheckInTime = dto.CheckInTime,
                CheckOutTime = dto.CheckOutTime,
                Status = dto.Status,
                Notes = dto.Notes
            };

            _unitOfWork.AttendanceRepo.Add(attendance);
            _unitOfWork.Save();
        }

        public void Update(string uid, UpdateAttendanceDto dto)
        {
            var attendance = _unitOfWork.AttendanceRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (attendance == null) return;

            attendance.MemberId = dto.MemberId;
            attendance.CheckInTime = dto.CheckInTime;
            attendance.CheckOutTime = dto.CheckOutTime;
            attendance.Status = dto.Status;
            attendance.Notes = dto.Notes;

            _unitOfWork.AttendanceRepo.Update(attendance);
            _unitOfWork.Save();
        }

        public void Delete(string uid)
        {
            var attendance = _unitOfWork.AttendanceRepo.GetAll().FirstOrDefault(x => x.UID == uid);
            if (attendance == null) return;

            _unitOfWork.AttendanceRepo.Delete(attendance);
            _unitOfWork.Save();
        }
    }
}