using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceTracker.Domain.Entity
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        // One Role → Many Users
        public ICollection<User> Users { get; set; }
    }
}
