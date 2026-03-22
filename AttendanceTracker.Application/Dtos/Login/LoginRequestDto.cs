using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
