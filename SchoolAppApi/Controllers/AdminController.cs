using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolAppApi.Data.Helpers;
using SchoolAppApi.Model;
using SchoolAppApi.Model.Library;
using SchoolAppApi.Model.ViewModel;
using SchoolAppApi.Services;
using System;

namespace SchoolAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = UserRoles.Manager)]
    public class AdminController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IStudentRepository _studentRepository;

        public AdminController(IBookRepository bookRepository, IStudentRepository studentRepository)
        {
            _bookRepository = bookRepository;
            _studentRepository = studentRepository;
        }

        [HttpGet("token-validation")]
        public async Task<IActionResult> ValidateToken()
        {
            return Ok("Token is valid");
        }



        [HttpGet("Students")]
        public async Task<IActionResult> GetStudents()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var students = await _studentRepository.GetStudents();
            return Ok(students);
        }

        [HttpGet("Student")]
        public async Task<IActionResult> GetStudentByName(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var students = await _studentRepository.GetStudentByName(name);
            return Ok(students);
        }

        [HttpGet("Student/{studentId}")]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.GetStudentExist(studentId))
                return NotFound("Student not found");

            var student = await _studentRepository.GetStudentById(studentId);


            return Ok(student);
        }

        [HttpGet("StudentDec")]
        public async Task<IActionResult> GetStudentsByDecending()
        {
            var students = await _studentRepository.GetStudentsByDecending();
            return Ok(students);
        }

        [HttpGet("Student-books")]
        public async Task<IActionResult> GetStudentBook()
        {
            var studentBooks = await _studentRepository.GetStudentsWithBooks();
            return Ok(studentBooks);
        }

        [HttpPost("create-student")]
        public async Task<IActionResult> CreateStudent(UserCreateVM userCreateVM)
        {
            if (userCreateVM == null)
                return BadRequest("Value can't be null");

            if (userCreateVM.Elementary == null)
            {
                return BadRequest("Elementary not found");
            }

            var student = new Student
            {
                Name = userCreateVM.Name,
                ImageUrl = userCreateVM.ImageUrl,
                Elementary = userCreateVM.Elementary
            };



            if ((int)student.Elementary > Enum.GetValues(typeof(ElementaryEnums)).Length - 1 )
                return NotFound("Elementary not Found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_studentRepository.CreateStudent(student))
                return BadRequest("Something wrong while creating");

            return Ok("Successfull Created");
        }

        [HttpPut("update-student")]
        public async Task<IActionResult> UpdateStudent(int studentId, [FromBody] Student student)
        {
            var currentStudent = await _studentRepository.GetStudentById(studentId);


            if (currentStudent == null)
            {
                return NotFound("Student not found");
            }

            // Apply changes to the currentBook using the updatedBookData
            if (!string.IsNullOrEmpty(student.Name))
            {
                currentStudent.Name = student.Name;
            }            
            
            if (!string.IsNullOrEmpty(student.ImageUrl))
            {
                currentStudent.ImageUrl = student.ImageUrl;
            }

            if (student.Elementary != null)
            {
                currentStudent.Elementary = student.Elementary;
            }

            if ((int)student.Elementary > Enum.GetValues(typeof(ElementaryEnums)).Length - 1)
            {
                return NotFound("Invalid elementary");
            }


            if (!_studentRepository.GetStudentExist(studentId))
            {
                return NotFound("Student not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_studentRepository.UpdateStudent(currentStudent))
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Updating Successful");
        }

        [HttpDelete("delete-student")]
        public async Task<IActionResult> DeleteStudent(int studentId)
        {
            if(studentId == null)
                return BadRequest("Input a valid char");

            if(!_studentRepository.GetStudentExist(studentId))
                return BadRequest("Student not found");

            var Id = await _studentRepository.GetStudentById(studentId);
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            if (!_studentRepository.DeleteStudent(Id))
                return BadRequest("Something wrong while deleting");

            return Ok("Successfull Deleted");
        }

        [HttpGet("books")]
        public async Task<IActionResult> GetBooks()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var books = await _bookRepository.GetBooks();
            return Ok(books);
        }

        [HttpGet("book")]
        public async Task<IActionResult> GetBookByName(string name)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepository.GetBook(name);
            return Ok(book);
        }



        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookRepository.BookExist(bookId))
                return NotFound("Book not found");

            var book = await _bookRepository.GetBookById(bookId);

            return Ok(book);
        }

        [HttpGet("book-user/{userId}")]
        public async Task<IActionResult> GetBookByUserId(int userId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book =  _bookRepository.GetBookByOwnerId(userId);
            return Ok(book);
        }

        [HttpPost("add-book")]
        public async Task<IActionResult> CreateBook([FromBody] BookCreateVM bookCreateVM)
        {
            if (string.IsNullOrWhiteSpace(bookCreateVM.Title))
                return BadRequest("Value can't be null");


            var book = new Book()
            {
                Title = bookCreateVM.Title,
                ImageUrl = bookCreateVM.ImageUrl,
                ISBN10 = bookCreateVM.ISBN10,
                ISBN13 = bookCreateVM.ISBN13,
                Series = bookCreateVM.Series,
                StudentId = bookCreateVM.StudentId
            };

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_bookRepository.CreateBook(book))
                return BadRequest("Something went wrong while creating the book");

            return Ok("Successfully created book");
        }

        [HttpPut("return-book")]
        public async Task<IActionResult> ReturnBook(int bookId)
        {
            var book = await _bookRepository.GetBookById(bookId);

            if (book == null)
            {
                return NotFound("Book Notfound");
            }

            if (book.StudentId == null)
            {
                return BadRequest("Student already return");
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            book.StudentId = null;

            if (!_bookRepository.UpdateBook(book))
                return BadRequest("Something went wrong while saving");

            return Ok("Successfull returned");
        }

        [HttpPut("update-book")]
        public async Task<IActionResult> UpdateBook(int bookId, int? studentId, [FromBody] Book updatedBookData)
        {

            var currentBook = await _bookRepository.GetBookById(bookId);

            if (currentBook == null)
            {
                return NotFound("Book not found");
            }

            if(studentId != null)
            {
                currentBook.StudentId = studentId;
                if (!_studentRepository.GetStudentExist(studentId))
                {
                    return NotFound("Student not found");
                }
            }


            // Apply changes to the currentBook using the updatedBookData
            if (!string.IsNullOrEmpty(updatedBookData.Title))
            {
                currentBook.Title = updatedBookData.Title;
            }
                        
            if (!string.IsNullOrEmpty(updatedBookData.ImageUrl))
            {
                currentBook.ImageUrl = updatedBookData.ImageUrl;
            }

            if (!string.IsNullOrEmpty(updatedBookData.ISBN10))
            {
                currentBook.ISBN10 = updatedBookData.ISBN10;
            }

            if (!string.IsNullOrEmpty(updatedBookData.ISBN13))
            {
                currentBook.ISBN13 = updatedBookData.ISBN13;
            }

            if (!string.IsNullOrEmpty(updatedBookData.Series))
            {
                currentBook.Series = updatedBookData.Series;
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_bookRepository.UpdateBook(currentBook))
            {
                return BadRequest("Something went wrong while saving");
            }

            return Ok("Updating Successful");
        }

        [HttpDelete("delete-book")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            if (bookId == null)
                return BadRequest("Input a valid char");

            if (!_bookRepository.BookExist(bookId))
                return BadRequest("Book not found");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var book = await _bookRepository.GetBookById(bookId);

            if (!_bookRepository.DeleteBook(book))
                return BadRequest("Something wrong while deleting");

            return Ok("Successfull Deleted");
        }

    }
}
