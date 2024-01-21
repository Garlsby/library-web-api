using SchoolAppApi.Model.Library;

namespace SchoolAppApi.Services
{
    public interface IStudentRepository
    {
        Task <ICollection<Student>> GetStudents();
        Task <ICollection<Student>> GetStudentsByDecending();
        Task <ICollection<Student>> GetStudentsWithBooks();
        Task <Student>? GetStudentById(int id);
        Task<ICollection<Student>>? GetStudentByName(string studentName);
        bool GetStudentExist(int? id);
        bool CreateStudent(Student student);
        bool UpdateStudent(Student student);
        bool DeleteStudent(Student student);
        bool Save();
    }
}
