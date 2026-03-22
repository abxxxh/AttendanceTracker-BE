using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.UserDetails
{
    public class UserDetailsDto
    {
        public string FullName { get; set; }
        public DateOnly DOB { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        public int Year { get; set; }
    }
}
