using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApp.Models
{
    [Table("Employee")]
    public class Employee
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Employee ID")]
        public int EmpId { get; set; }


        [Required]
        [Column(TypeName = "varchar(5)")]
        [MaxLength(5)]
        [Display(Name = "Employee Number")]
        public string EmpNum { get; set; }


        [Required]
        [Column(TypeName = "varchar(30)")]
        [MaxLength(30)]
        [Display(Name = "Employee Name")]
        public string EmpName { get; set; }

        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime DOB { get; set; }


        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Hiring Date")]
        [DisplayFormat(DataFormatString = "{0:dd-MMM-yyyy}")]
        public DateTime HiringDate { get; set; }


        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Gross Salary")]
        public decimal GrossSalary { get; set; }


        [Required]
        [Column(TypeName = "decimal(12,2)")]
        [Display(Name = "Net Salary")]
        public decimal NetSalary { get; set; }


        [Required]
        [ForeignKey("Department")]
        [Display(Name = "Department ID")]
        public int DepartmentId { get; set; }


        [Display(Name = "Department Name")]
        [NotMapped]
        public string DepartmentName { get; set; }


        public virtual Department Department { get; set; }
    }
}
