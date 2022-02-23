using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class Context : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        public string DbPath { get; }

        public Context() => DbPath = "server=localhost;user=root;database=praunote;password=;";

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(DbPath, new MySqlServerVersion(new Version(5, 7, 36)));
    }
}
