using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Login
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string RoleName { get; set; }
    }
}
