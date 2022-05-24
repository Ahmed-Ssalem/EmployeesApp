using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApp.Models
{
    public class HR_DBContext : DbContext
    {
        public HR_DBContext()
        {
        }

        public HR_DBContext(DbContextOptions<HR_DBContext> options) : base(options)
        {

        }


        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }


        

    }
}
