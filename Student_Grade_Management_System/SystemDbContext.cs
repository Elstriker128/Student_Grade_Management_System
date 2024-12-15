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
        public DbSet<Models.GradeType> GradeTypes { get; set;}
        public DbSet<Models.GradeWeight> GradeWeights { get; set; }
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
        public DbSet<Models.Schedule> Timetables { get; set; }
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
            modelBuilder.Entity<Models.ParentOfStudent>().ToTable("mokinio_tevas").HasKey(ps => new {ps.Student_Username, ps.Parent_Username});
            modelBuilder.Entity<Models.Student>().ToTable("mokinys").HasKey(s => s.Username);


			// Add the conversion for DateOnly
			modelBuilder.Entity<Models.Student>()
				.Property(s => s.Birth_Date)
				.HasConversion(
					v => v.ToDateTime(TimeOnly.MinValue),  // Convert DateOnly to DateTime (stored in the database)
					v => DateOnly.FromDateTime(v)           // Convert DateTime back to DateOnly when reading from the database
				);

			modelBuilder.Entity<Models.School>().ToTable("mokykla").HasKey(s => s.ID);
            modelBuilder.Entity<Models.Teacher>().ToTable("mokytojas").HasKey(t => t.Username);
            modelBuilder.Entity<Models.SubjectOfTeacher>().ToTable("mokytojo_dalykas").HasKey(st => new { st.Teacher_Username, st.Subject_ID });

            modelBuilder.Entity<Models.SubjectOfTeacher>()
                .HasOne(st => st.Teacher)
                .WithMany()
                .HasForeignKey(st => st.Teacher_Username);

            modelBuilder.Entity<Models.SubjectOfTeacher>()
                .HasOne(st => st.Subject)
                .WithMany()
                .HasForeignKey(st => st.Subject_ID);
            modelBuilder.Entity<Models.SchoolOfTeacher>().ToTable("mokytojo_mokykla").HasKey(st => new {st.Teacher_Username, st.School_ID});
            modelBuilder.Entity<Models.Lesson>().ToTable("pamoka").HasKey(l => l.ID);
            modelBuilder.Entity<Models.Parent>().ToTable("tevas").HasKey(p => p.Username);
            modelBuilder.Entity<Models.Schedule>().ToTable("tvarkarastis").HasKey(tt => new {tt.Lesson_ID, tt.Class_Letter, tt.Class_Number});
        }
    }

}