using System.ComponentModel.DataAnnotations;

namespace SchoolAppApi.Model.ViewModel
{
    public class LoginVM
    {
        public string EmailAddress { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
