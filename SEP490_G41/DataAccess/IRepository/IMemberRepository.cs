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
        public List<MemberDTO> GetAllMembers();
        public void AddNewMember(AddMemberDTO member);
        public bool DeleteMember(int id);
        public List<MemberDTO> SearchMemberByName(string name);
        public List<MemberDTO> SearchMemberByDoB(DateTime date);
        public List<MemberDTO> SearchMemberByStatus(string status);
        public bool Login(string username, string password);
        public bool ChangePassword(string oldPass, string newPass);
    }
}
