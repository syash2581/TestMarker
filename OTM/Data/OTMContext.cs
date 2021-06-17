using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OTM.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace OTM.Data
{
    public class OTMContext : IdentityDbContext<CustomIdentity>
    {
        public OTMContext(DbContextOptions<OTMContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<StudentTest>().HasKey(sc => new { sc.StudentId, sc.TestId });
            builder.Entity<Student>().HasIndex(e => e.rollno).IsUnique(true);
            builder.Entity<Faculty>().HasIndex(e => e.rollno).IsUnique(true);
            builder.Entity<Student>().HasOne(p => p.department).WithMany(b => b.student).HasForeignKey(p => p.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Faculty>().HasOne(p => p.department).WithMany(b => b.Faculty).HasForeignKey(p => p.DepartmentId).OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Student>().HasOne(p => p.semester).WithMany(b => b.students).HasForeignKey(p => p.SemesterId).OnDelete(DeleteBehavior.NoAction);
            
        }
        public DbSet<Semester> semesters { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Options> Options { get; set; }
        public DbSet<StudentTest> StudentTests { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<OTM.Models.TestResults> TestResults { get; set; }

    }
}
