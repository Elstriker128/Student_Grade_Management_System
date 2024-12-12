using Microsoft.EntityFrameworkCore;

namespace Student_Grade_Management_System
{
    public class SystemDbContext : DbContext
    {
        public SystemDbContext(DbContextOptions<SystemDbContext> options) : base(options)
        {
        }

        public DbSet<Models.Administrator> Administrators { get; set; }
        public DbSet<Models.Class> Classes { get; set; }
        public DbSet<Models.Grade> Grades { get; set; }
        public DbSet<Models.Lesson> Lessons { get; set; }
        public DbSet<Models.ParentOfStudent> ParentsOfStudents { get; set; }
        public DbSet<Models.Parent> Parents { get; set; }
        public DbSet<Models.Review> Reviews { get; set; }
        public DbSet<Models.ReviewType> ReviewTypes { get; set; }
        public DbSet<Models.Student> Students { get; set; }
        public DbSet<Models.School> Schools { get; set; }
        public DbSet<Models.SchoolOfTeacher> SchoolsOfTeachers { get; set; }
        public DbSet<Models.Teacher> Teachers { get; set; }
        public DbSet<Models.Subject> Subjects { get; set; }
        public DbSet<Models.SubjectOfTeacher> SubjectsOfTeachers { get; set; }
        public DbSet<Models.Timetable> Timetables { get; set; }
        public DbSet<Models.WorkDay> WorkDays { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Administrator>().ToTable("administratorius").HasKey(a => a.Username);
            modelBuilder.Entity<Models.Review>().ToTable("atsiliepimas").HasKey(r => r.ID);
            modelBuilder.Entity<Models.ReviewType>().ToTable("atsiliepimo_tipas").HasKey(rt => rt.ID);
            modelBuilder.Entity<Models.Subject>().ToTable("dalykas").HasKey(s => s.ID);
            modelBuilder.Entity<Models.WorkDay>().ToTable("darbo_diena").HasKey(wd => wd.ID);
            modelBuilder.Entity<Models.Grade>().ToTable("ivertinimas").HasKey(g => g.ID);
            modelBuilder.Entity<Models.GradeWeight>().ToTable("ivertinimo_svoris").HasKey(gw => gw.ID);
            modelBuilder.Entity<Models.GradeType>().ToTable("ivertinimo_tipas").HasKey(gt => gt.ID);
            modelBuilder.Entity<Models.Class>().ToTable("klase").HasKey(c => new {c.Number, c.Letter});
            modelBuilder.Entity<Models.ParentOfStudent>().ToTable("mokinio_tevas").HasNoKey(); 
            modelBuilder.Entity<Models.Student>().ToTable("mokinys").HasKey(s => s.Username);
            modelBuilder.Entity<Models.School>().ToTable("mokykla").HasKey(s => s.ID);
            modelBuilder.Entity<Models.Teacher>().ToTable("mokytojas").HasKey(t => t.Username);
            modelBuilder.Entity<Models.SubjectOfTeacher>().ToTable("mokytojo_dalykas").HasNoKey();
            modelBuilder.Entity<Models.SchoolOfTeacher>().ToTable("mokytojo_mokykla").HasNoKey();
            modelBuilder.Entity<Models.Lesson>().ToTable("pamoka").HasKey(l => l.ID);
            modelBuilder.Entity<Models.Parent>().ToTable("tevas").HasKey(p => p.Username);
            modelBuilder.Entity<Models.Timetable>().ToTable("tvarkarastis").HasNoKey();
        }
    }

}