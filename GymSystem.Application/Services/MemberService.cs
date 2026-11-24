using GymSystem.Application.Dtos.MemberDtos;
using GymSystem.Application.Services;
using GymSystem.Domain.Models;
using GymSystem.Infrastructure.Repositories.Base;

namespace GymSystem.Application.Services
{
    public class MemberService : IMemberService
{
    private readonly IUnitOfWork _unitOfWork;

    public MemberService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public IEnumerable<MemberDto> GetAll()
    {
        var members = _unitOfWork.MemberRepo.GetAll();

        return members.Select(m => new MemberDto
        {
            Id = m.Id,
            UID = m.UID,
            Name = m.Name,
            Email = m.Email,
            Phone = m.Phone
        }).ToList();
    }

    public MemberDto? GetById(int id)
    {
        var m = _unitOfWork.MemberRepo.GetById(id);
        if (m == null) return null;

        return new MemberDto
        {
            Id = m.Id,
            UID = m.UID,
            Name = m.Name,
            Email = m.Email,
            Phone = m.Phone
        };
    }

    public MemberDto? GetByUid(string uid)
    {
        var m = _unitOfWork.MemberRepo.GetAll().FirstOrDefault(x => x.UID == uid);
        if (m == null) return null;

        return new MemberDto
        {
            Id = m.Id,
            UID = m.UID,
            Name = m.Name,
            Email = m.Email,
            Phone = m.Phone
        };
    }

    public void Add(CreateMemberDto dto)
    {
        var member = new Member
        {
            UID = string.IsNullOrWhiteSpace(dto.UID) ? System.Guid.NewGuid().ToString() : dto.UID,
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone
        };

        _unitOfWork.MemberRepo.Add(member);
        _unitOfWork.Save();
    }

    public void Update(string uid, UpdateMemberDto dto)
    {
        var member = _unitOfWork.MemberRepo.GetAll().FirstOrDefault(x => x.UID == uid);
        if (member == null) return;

        member.Name = dto.Name;
        member.Email = dto.Email;
        member.Phone = dto.Phone;

        _unitOfWork.MemberRepo.Update(member);
        _unitOfWork.Save();
    }

    public void Delete(string uid)
    {
        var member = _unitOfWork.MemberRepo.GetAll().FirstOrDefault(x => x.UID == uid);
        if (member == null) return;

        _unitOfWork.MemberRepo.Delete(member);
        _unitOfWork.Save();
    }
}
}
