using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolAppApi.Model.Library
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string ISBN13 { get; set; } = string.Empty;
        public string ISBN10 { get; set; } = string.Empty;

        //relationship

        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}
