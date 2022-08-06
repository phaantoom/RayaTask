using System.ComponentModel.DataAnnotations;

namespace RayaTask.ViewModel
{
    public class CreateRole
    {
        [Required]
        public string Name { get; set; }
    }
}
