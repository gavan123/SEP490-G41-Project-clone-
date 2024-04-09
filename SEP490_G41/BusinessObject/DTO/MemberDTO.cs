﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class MemberDTO
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
    public class MemberUpdateDTO
    {
        public int MemberId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Status { get; set; } = null!;

    }
    public class ChangePasswordDTO
    {
        public int MemberId { get; set; }
        public string Password { get; set; } = null!;
    }
}
