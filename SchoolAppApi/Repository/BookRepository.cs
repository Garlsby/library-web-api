using Microsoft.EntityFrameworkCore;
using SchoolAppApi.Data;
using SchoolAppApi.Model.Library;
using SchoolAppApi.Services;
using System.Collections.Immutable;

namespace SchoolAppApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly StudentDbContext _context;

        public BookRepository(StudentDbContext context) 
        {
            _context = context;
        }

        public bool BookExist(int id)
        {
            return _context.Books.Any(x => x.Id == id);
        }

        public bool CreateBook(Book book)
        {
            _context.Add(book);
            return Save();
        }

        public bool DeleteBook(Book book)
        {
            _context.Remove(book);
            return Save();
        }

        public async Task<ICollection<Book>> GetBook(string BookName)
        {
            return await _context.Books.Where(b => b.Title.ToLower().Contains(BookName.ToLower())).ToListAsync();
        }

        public async Task<Book>? GetBookById(int bookId)
        {
            return await _context.Books.Where(b => b.Id == bookId).Include(s => s.Student).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Book>> GetBooks()
        {
            return await _context.Books
                .Select(b => new Book
                {
                    Id = b.Id,
                    Title = b.Title,
                    ImageUrl = b.ImageUrl,
                    Series = b.Series,
                    ISBN13 = b.ISBN13,
                    ISBN10 = b.ISBN10,
                    StudentId = b.StudentId,
                    Student = b.Student != null ? new Student
                    {
                        Id = b.Student.Id,
                        Name = b.Student.Name,
                        ImageUrl = b.Student.ImageUrl,
                        Elementary = b.Student.Elementary
                    } : null
                })
                .ToListAsync();
        }



        public int? GetOwnerByBookId(int bookId)
        {
            return _context.Books
                .Where(book => book.Id == bookId)
                .Select(book => book.StudentId)
                .FirstOrDefault();
        }        
        
        public async Task<ICollection<Book>> GetBookByOwnerId(int ownerId)
        {
            return _context.Books
                .Where(b => b.StudentId == ownerId)
                .ToList();
        }


        public bool Save()
        {
                var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateBook(Book book)
        {
            _context.Update(book);
            return Save();
        }
    }
}
