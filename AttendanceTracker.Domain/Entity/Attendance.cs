using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceTracker.Domain.Entity
{
    public class Attendance
    {
        [Key]
        public int AttendanceID { get; set; }

        public int UserId { get; set; }
       

        public DateOnly Date { get; set; }
        public string Status { get; set; }
        public string Course { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RecordedBy")]
        public User RecordedByUser { get; set; }

        public int RecordedBy { get; set; }

    }
}
