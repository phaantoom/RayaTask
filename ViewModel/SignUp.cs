using System.ComponentModel.DataAnnotations;

namespace RayaTask.ViewModel
{
    public class SignUp
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm Password And Password don't match")]
        public string ConfirmPassword { get; set; }
        public string role { get; set; }
    }
}
