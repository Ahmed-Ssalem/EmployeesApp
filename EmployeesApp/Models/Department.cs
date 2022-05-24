using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApp.Models
{
    [Table("Department")]
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Department ID")]
        public int DeptId { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        [Display(Name = "Department Name")]
        public string DeptName { get; set; }

        [Required]
        [Column(TypeName = "varchar(5)")]
        [Display(Name = "Department Abbreviation")]
        public string DeptAbbr { get; set; }
    }
}
