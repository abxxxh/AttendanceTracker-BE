using AttendanceTracker.Application.Dtos.UserDetails;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.User
{
    public class UserRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public UserDetailsDto userdetailsnavigation { get; set; }
    }
}
