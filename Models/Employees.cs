using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RayaTask.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public decimal Salary { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime HiringDate { get; set; }
        public string Status { get; set; }

    }
}
