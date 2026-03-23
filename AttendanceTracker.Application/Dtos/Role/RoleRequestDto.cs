using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Role
{
    public class RoleRequestDto
    {
        //public int RoleId { get; set; }


        public string RoleName { get; set; }


        public string Description { get; set; } = string.Empty;
    }
}
