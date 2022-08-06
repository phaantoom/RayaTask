using System.ComponentModel.DataAnnotations;

namespace RayaTask.ViewModel
{
    public class SignIn
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
