using System.ComponentModel.DataAnnotations;

namespace MyWebApplication.Models
{
    public class LoginViewModel
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        [UIHint("password")]   
        public string? Password { get; set; }

        [Display(Name = "Запомнить меня?")]
        public bool  RememberMe { get; set; }
    }
}
