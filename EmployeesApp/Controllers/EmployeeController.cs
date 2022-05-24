using EmployeesApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesApp.Controllers
{ 

    public enum SortDirection
    {
        Ascending,
        Descending
    }

    public class EmployeeController : Controller
    {
        public HR_DBContext _dBContext { get; set; }

        public EmployeeController(HR_DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        public IActionResult Index(string SortField, string CurrentSortField, SortDirection SortDirection, string SearchByName)
        {
            var employees = GetEmployees();

            if (!string.IsNullOrEmpty(SearchByName))
                employees = employees.Where(e => e.EmpName.ToLower().Contains(SearchByName.ToLower())).ToList();
            return View(SortEmployees(employees, SortField, CurrentSortField, SortDirection));
        }


        public IActionResult Create()
        {
            ViewBag.Departments = _dBContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(Employee emp)
        {
            ModelState.Remove("EmpId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");

            if (ModelState.IsValid)
            {
                _dBContext.Employees.Add(emp);
                _dBContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _dBContext.Departments.ToList();
            return View();
        }

        public IActionResult Edit(int id)
        {
            var emp = _dBContext.Employees.Where(e => e.EmpId == id).FirstOrDefault();
            ViewBag.Departments = _dBContext.Departments.ToList();
            return View("Create", emp);
        }

        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            ModelState.Remove("EmpId");
            ModelState.Remove("Department");
            ModelState.Remove("DepartmentName");

            if (ModelState.IsValid)
            {
                _dBContext.Employees.Update(emp);
                _dBContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Departments = _dBContext.Departments.ToList();
            return View("Create", emp);
        }
        public IActionResult Delete (int id)
        {
            var emp = _dBContext.Employees.Where(e => e.EmpId == id).FirstOrDefault();
            if (emp != null)
            {
                _dBContext.Employees.Remove(emp);
                _dBContext.SaveChanges();
            }

            return RedirectToAction("Index");
        }


        public List<Employee> SortEmployees(List<Employee> employees, string sortField, string currentSortField, SortDirection sortDirection)
        {
            if (string.IsNullOrEmpty(sortField))
            {
                ViewBag.SortField = "EmpNum";
                ViewBag.SortDirection = SortDirection.Ascending;
            }
            else
            {
                if (sortField == currentSortField)
                    ViewBag.SortDirection = sortDirection == SortDirection.Ascending ? SortDirection.Descending : SortDirection.Ascending;
                else
                    ViewBag.SortDirection = SortDirection.Ascending;

                ViewBag.SortField = sortField;
            }

            var propertyInfo = typeof(Employee).GetProperty(ViewBag.SortField);
            if (ViewBag.SortDirection == SortDirection.Ascending)
                employees = employees.OrderBy(e => propertyInfo.GetValue(e, null)).ToList();
            else
                employees = employees.OrderByDescending(e => propertyInfo.GetValue(e, null)).ToList();

            return employees;
        }



        private List<Employee> GetEmployees()
        {
            var employees = (from Employee e in _dBContext.Employees
                             join Department d in _dBContext.Departments on e.DepartmentId equals d.DeptId
                             select new Employee
                             {
                                 EmpId = e.EmpId,
                                 EmpName = e.EmpName,
                                 EmpNum = e.EmpNum,
                                 DOB = e.DOB,
                                 HiringDate = e.HiringDate,
                                 GrossSalary = e.GrossSalary,
                                 NetSalary = e.NetSalary,
                                 DepartmentId = e.DepartmentId,
                                 DepartmentName = d.DeptName
                             }).ToList();

            return employees;
        }



    }
}
