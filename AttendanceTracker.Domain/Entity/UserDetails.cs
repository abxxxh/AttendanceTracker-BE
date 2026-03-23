using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AttendanceTracker.Domain.Entity
{
    //[Index(nameof(UserId), IsUnique = true)]
    public class UserDetails
    {
        [Key]
        public int Id { get; set; }


        public int UserId { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public DateOnly DOB {  get; set; }

        [Required]
        public string Gender {  get; set; }

        [Required]
        public string PhoneNumber {  get; set; }


        [Required]
        public string Address { get; set; }

        [Required]
        public string Department { get; set; }

        [Required]
        public int Year { get; set; }

        [ForeignKey("UserId")]
        public User User { get ; set; }


    }
}
