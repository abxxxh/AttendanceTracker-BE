using AttendanceTracker.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace AttendanceTracker.Infrastructure.DbContext
{
    public class AttendanceTrackerDbContext:Microsoft.EntityFrameworkCore.DbContext
    {
        public AttendanceTrackerDbContext(DbContextOptions<AttendanceTrackerDbContext> options):base(options)
        {
            
        }

        public DbSet<User> UsersTable { get; set; }
        public DbSet<Role> RolesTable { get; set; }
        public DbSet<Attendance> AttendancesTable { get; set; }
        public DbSet<UserDetails> UserDetailsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // good practice

            // 🔥 One-to-One (User ↔ UserDetails)
            modelBuilder.Entity<UserDetails>()
                .HasIndex(u => u.UserId)
                .IsUnique();

            // 🔥 Attendance → User (UserId)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.User)
                .WithMany(u => u.Attendances)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // 🔥 Attendance → User (RecordedBy)
            modelBuilder.Entity<Attendance>()
                .HasOne(a => a.RecordedByUser)
                .WithMany(u => u.RecordedAttendances)
                .HasForeignKey(a => a.RecordedBy)
                .OnDelete(DeleteBehavior.Restrict);


            ////extra for one user can mark attendance only once per day
            modelBuilder.Entity<Attendance>()
    .HasIndex(a => new { a.UserId, a.Date,a.Course })
    .IsUnique();



        }
    }
}
