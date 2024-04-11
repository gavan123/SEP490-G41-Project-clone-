using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Member
    {
        public Member()
        {
            Mapmanages = new HashSet<Mapmanage>();
        }

        public int MemberId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DoB { get; set; }
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Status { get; set; } = null!;
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Mapmanage> Mapmanages { get; set; }
    }
}
