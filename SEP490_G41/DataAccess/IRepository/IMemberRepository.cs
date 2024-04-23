using BusinessObject.DTO;
using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IMemberRepository
    {
        List<MemberDTO> GetAllMembers();
        void AddNewMember(AddMemberDTO member);
        bool DeleteMember(int id);
        List<MemberDTO> SearchMemberByName(string name);
        public MemberDTO GetMemberByEmail(string email);
        public string SendCode(string email);
        /*public List<MemberDTO> SearchMemberByStatus(string status);*/
        MemberDTO Login(string username, string password);
        string ChangePassword(int id, ChangePasswordModel changePass);
        void UpdateMemberStatus(MemberStatusDTO member);
        public bool ResetPassword(int id, string newpass);
    }
}
