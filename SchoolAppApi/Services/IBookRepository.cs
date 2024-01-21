using SchoolAppApi.Model.Library;

namespace SchoolAppApi.Services
{
    public interface IBookRepository
    {
        Task<ICollection<Book>> GetBooks();
        Task<ICollection<Book>> GetBook(string BookName);
        Task<Book>? GetBookById(int bookId);
        int? GetOwnerByBookId(int bookId);
        Task<ICollection<Book>> GetBookByOwnerId(int ownerId);
        bool CreateBook(Book book);
        bool UpdateBook(Book book);
        bool DeleteBook(Book book);
        bool BookExist(int id);
        bool Save();
    }
}
