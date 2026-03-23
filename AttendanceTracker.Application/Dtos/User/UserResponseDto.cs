using AttendanceTracker.Application.Dtos.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.User
{
    public class UserResponseDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }

        public UserDetailsDto UserDetails { get; set; }
    }
}
