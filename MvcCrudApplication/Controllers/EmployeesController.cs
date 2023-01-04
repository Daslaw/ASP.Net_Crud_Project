using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcCrudApplication.Data;
using MvcCrudApplication.Models;
using MvcCrudApplication.Models.Domain;

namespace MvcCrudApplication.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCProjectDbContext mvcProjectDbContext;

        public EmployeesController(MVCProjectDbContext mvcProjectDbContext)
        {
            this.mvcProjectDbContext = mvcProjectDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employee = await mvcProjectDbContext.Employees.ToListAsync();
            
            return View(employee);

        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateOfBirth = addEmployeeRequest.DateOfBirth
            };

            await mvcProjectDbContext.Employees.AddAsync(employee);

            await mvcProjectDbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpGet]

        public async Task<IActionResult> View(Guid id)
        {
           var employee = await mvcProjectDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null)
            {

                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth
                };

                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcProjectDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcProjectDbContext.SaveChangesAsync();
                  
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]

         public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcProjectDbContext.Employees.FindAsync(model.Id);
            if (employee != null)
            {
                mvcProjectDbContext.Employees.Remove(employee);

                await mvcProjectDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");

        }

    }
} 