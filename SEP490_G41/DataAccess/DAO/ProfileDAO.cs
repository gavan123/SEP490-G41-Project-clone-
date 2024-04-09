﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAO
{
    

    public class ProfileDAO
    {
        private readonly finsContext _context;
        public ProfileDAO(finsContext context)
        {
            _context = context;
        }

        // Đọc thông profile members nhà bằng Id
        public Member GetMemberById(int memberid)
        {
            return _context.Members.FirstOrDefault(m => m.MemberId == memberid);
        }

        // Cập nhật thông tin Profile
        public void UpdateProfile(Member member)
        {
            var existingMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);

            if (existingMember != null)
            {
                existingMember.FullName = member.FullName;
                existingMember.DoB = member.DoB;
                existingMember.Address = member.Address;
                existingMember.Phone = member.Phone;
                existingMember.Email = member.Email;
                existingMember.Username = member.Username;             
                existingMember.Status = member.Status;      

                _context.SaveChanges();
            }
        }
        // Change password
        public void ChangePassword(Member member)
        {
            var existingMember = _context.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if(existingMember != null)
            {
                existingMember.Password = member.Password;
                _context.SaveChanges();
            }
        }

    }
}
