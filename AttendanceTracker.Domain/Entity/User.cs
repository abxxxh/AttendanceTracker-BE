using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceTracker.Domain.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; }=string.Empty;

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;

        public UserDetails UserDetails { get; set; }

        // 🔥 Add these
        public ICollection<Attendance> Attendances { get; set; }
        public ICollection<Attendance> RecordedAttendances { get; set; }
    }
}
