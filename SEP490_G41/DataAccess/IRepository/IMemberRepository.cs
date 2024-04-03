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
        List<Member> GetAllMembers();
        List<Member> SearchMemberByName(string name);
        bool Login(string username,string password);
        bool AddNewMember();
        bool UpdateMember(Member member);
        bool DeleteMember(string name);
    }
}
