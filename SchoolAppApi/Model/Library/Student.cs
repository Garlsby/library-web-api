namespace SchoolAppApi.Model.Library
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public ElementaryEnums Elementary { get; set; }

        //relationship

        public ICollection<Book>? Books { get; set; }
    }
}
