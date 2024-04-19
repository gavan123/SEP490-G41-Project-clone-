using System;
using System.Collections.Generic;

namespace BusinessObject.Models
{
    public partial class Role
    {
        public Role()
        {
            Members = new HashSet<Member>();
        }

        public int RoleId { get; set; }
<<<<<<< HEAD
        public string RoleName { get; set; }
        public string Description { get; set; }
=======
        public string RoleName { get; set; } = null!;
        public string? Description { get; set; }
>>>>>>> main

        public virtual ICollection<Member> Members { get; set; }
    }
}
