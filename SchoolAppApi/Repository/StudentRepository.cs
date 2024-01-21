using Microsoft.EntityFrameworkCore;
using SchoolAppApi.Data;
using SchoolAppApi.Model.Library;
using SchoolAppApi.Services;

namespace SchoolAppApi.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDbContext _context;

        public StudentRepository(StudentDbContext context)
        {
            _context = context;
        }

        public bool CreateStudent(Student student)
        {
            _context.Add(student);
            return Save();
        }

        public bool DeleteStudent(Student student)
        {
            _context.Remove(student);
            return Save();
        }

        public async Task<Student>? GetStudentById(int id)
        {
            return await _context.Student.Where(s => s.Id == id).Include(b => b.Books).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Student>> GetStudentByName(string StudentName)
        {
            return await _context.Student.Where(s => s.Name.ToLower().Contains(StudentName.ToLower())).Include(s => s.Books).ToListAsync();
        }

        public bool GetStudentExist(int? id)
        {
           return _context.Student.Any(x => x.Id == id);
        }

        public async Task<ICollection<Student>> GetStudents()
        {
           return _context.Student.Include(b => b.Books).OrderBy(n => n.Name).ToList();
        }

        public async Task<ICollection<Student>> GetStudentsByDecending()
        {
            return _context.Student.OrderByDescending(b => b.Id).Include(b => b.Books).ToList();
        }

        public async Task<ICollection<Student>> GetStudentsWithBooks()
        {
            var studentWithBook = _context.Student
                .Include(s => s.Books)
                .Where(s => s.Books != null && s.Books.Any())
                .ToList();

            return (studentWithBook); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateStudent(Student student)
        {   
            _context.Update(student);
            return Save();
        }
    }
}
