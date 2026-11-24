using System;

namespace GymSystem.Application.Dtos.MemberDtos
{
    public class CreateMemberDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string UID { get; set; } = Guid.NewGuid().ToString();
    }

    public class UpdateMemberDto : CreateMemberDto
    {
        public int Id { get; set; }
    }

    public class MemberDto : UpdateMemberDto
    {
        
    }
}