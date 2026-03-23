using AttendanceTracker.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Role
{
    public class RoleResponseDto
    {
        public int RoleId { get; set; }

     
        public string RoleName { get; set; }

    
        public string Description { get; set; } = string.Empty;

       
    }
}
