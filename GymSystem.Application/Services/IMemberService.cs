using System.Collections.Generic;
using GymSystem.Application.Dtos.MemberDtos;

namespace GymSystem.Application.Services
{
    public interface IMemberService
    {
        IEnumerable<MemberDto> GetAll();
        MemberDto? GetById(int id);
        MemberDto? GetByUid(string uid);
        void Add(CreateMemberDto dto);
        void Update(string uid, UpdateMemberDto dto);
        void Delete(string uid);
    }
}