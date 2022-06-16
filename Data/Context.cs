using AuthModels;
using Microsoft.EntityFrameworkCore;
using Models;
using System;

namespace Data
{
    public class Context : DbContext
    {
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<PersonSetting> PersonSettings { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<User> Users { get; set; }
        
        public string DbPath { get; }

        // public Context() => DbPath = "server=localhost;user=root;database=praunote;password=;";
        public Context() => DbPath = "praunote.db";

        //protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseMySql(DbPath, new MySqlServerVersion(new Version(5, 7, 36)));
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
