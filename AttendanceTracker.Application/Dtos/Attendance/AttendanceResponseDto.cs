using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Application.Dtos.Attendance
{
    public class AttendanceResponseDto
    {
        public int AttendanceID { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateOnly Date { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }
        public int RecordedBy { get; set; }
        public string RecordedByName { get; set; }
    }
}
