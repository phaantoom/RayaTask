using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RayaTask.Data;
using RayaTask.Models;
using RayaTask.ViewModel;
using System;
using System.Collections.Generic;

namespace RayaTask.Controllers
{
    [Authorize]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _appData;

        public EmployeesController(ApplicationDbContext applicationDbContext)
        {
            _appData = applicationDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LoadGrid()
        {
            IEnumerable<Employees> employees = _appData.Employees;

            return Json(new { data = employees });
        }
        public IActionResult NewEmployee(string name, string telephone, string salary, string date)
        {
            if (User.IsInRole("HRAdmin"))
            {
                _appData.Employees.Add(new Employees { HiringDate = DateTime.Parse(date), Name = name, Telephone = telephone, Salary = decimal.Parse(salary), Status = "Approved" });
                _appData.SaveChanges();
            }
            else
            {
                _appData.Employees.Add(new Employees { HiringDate = DateTime.Parse(date), Name = name, Telephone = telephone, Salary = decimal.Parse(salary), Status = "New" });
                _appData.SaveChanges();
            }

            return Json(new { data = true });
        }
        [HttpGet]
        public IActionResult GetEmployee(string Id)
        {
            int ID = int.Parse(Id);
            Employees employee = _appData.Employees.Find(ID);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                GetEmployee getEmployee = new()
                {
                    Name = employee.Name,
                    Id = employee.Id,
                    Salary = employee.Salary,
                    Telephone = employee.Telephone,
                    HiringDate = employee.HiringDate.ToString("yyyy-MM-dd"),
                    Status = employee.Status
                };

                return Json(new { data = getEmployee });
            }
        }
        [HttpPost]
        public IActionResult EditEmployee(string Id, string name, string telephone, string salary, string date)
        {
            int ID = int.Parse(Id);
            Employees employee = _appData.Employees.Find(ID);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.Name = name;
                employee.Telephone = telephone;
                employee.Salary = decimal.Parse(salary);
                employee.HiringDate = DateTime.Parse(date);

                _appData.Employees.Update(employee);
                _appData.SaveChanges();
                return Json(new { data = true });
            }
        }
        [HttpPost]
        public IActionResult ApproveEmployee(string Id)
        {
            int ID = int.Parse(Id);
            Employees employee = _appData.Employees.Find(ID);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                employee.Status = "Approved";
                _appData.Employees.Update(employee);
                _appData.SaveChanges();

                return Json(new { data = true });
            }
        }
        [HttpPost]
        public IActionResult DeleteEmployee(string Id)
        {
            int ID = int.Parse(Id);
            Employees employee = _appData.Employees.Find(ID);
            if (employee == null)
            {
                return NotFound();
            }
            else{
                _appData.Employees.Remove(employee);
                _appData.SaveChanges();

                return Json(new { data = true });
            }
        }
    }
}
