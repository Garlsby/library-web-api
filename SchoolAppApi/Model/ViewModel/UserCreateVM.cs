using System.ComponentModel.DataAnnotations;

namespace SchoolAppApi.Model.ViewModel
{
    public class UserCreateVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        [Required]
        public ElementaryEnums Elementary { get; set; }
    }
}
