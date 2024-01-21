using Microsoft.EntityFrameworkCore;
using SchoolAppApi.Model.Library;

namespace SchoolAppApi.Data
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions<StudentDbContext> options) : base(options) 
        {
        }

        public DbSet<Student> Student { get; set; }
        public DbSet<Book> Books{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(s => s.Student)
                .WithMany(s=> s.Books)
                .HasForeignKey(book => book.StudentId);
        }
    }
}
