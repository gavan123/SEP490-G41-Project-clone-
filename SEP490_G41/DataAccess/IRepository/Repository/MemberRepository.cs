using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public bool AddNewMember()
        {
            throw new NotImplementedException();
        }

        public bool DeleteMember(string name)
        {
            throw new NotImplementedException();
        }

        public List<Member> GetAllMembers()
        {
            throw new NotImplementedException();
        }

        public bool Login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public List<Member> SearchMemberByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMember(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
