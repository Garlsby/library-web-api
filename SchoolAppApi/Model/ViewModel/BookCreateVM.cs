namespace SchoolAppApi.Model.ViewModel
{
    public class BookCreateVM
    {
        public string Title { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;

        public string ISBN13 { get; set; } = string.Empty;
        public string ISBN10 { get; set; } = string.Empty;

        //relationship
        public int? StudentId { get; set; } = null!;
    }
}
