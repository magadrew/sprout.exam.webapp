using Microsoft.EntityFrameworkCore;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Data;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var result = await _dbContext.Employee.Where(data => data.IsDeleted == false).ToListAsync();

            return result;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            var result = await _dbContext.Employee.Where(data => data.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Employee> CreateEmployee(CreateEmployeeDto employee)
        {
            Employee newEmployee = new Employee();
            newEmployee.FullName = employee.FullName;
            newEmployee.Birthdate = employee.Birthdate;
            newEmployee.EmployeeTypeId = employee.EmployeeTypeId;
            newEmployee.Tin = employee.Tin;

            _dbContext.Employee.Add(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task<Employee> EditEmployee(EditEmployeeDto employee)
        {
            Employee newEmployee = new Employee();
            newEmployee.Id = employee.Id;
            newEmployee.FullName = employee.FullName;
            newEmployee.Birthdate = employee.Birthdate;
            newEmployee.EmployeeTypeId = employee.EmployeeTypeId;
            newEmployee.Tin = employee.Tin;

            _dbContext.Employee.Update(newEmployee);
            await _dbContext.SaveChangesAsync();

            return newEmployee;
        }

        public async Task<Employee> RemoveEmployee(int id)
        {
            var employee = _dbContext.Employee.First(data => data.Id == id);

            employee.IsDeleted = true;

            await _dbContext.SaveChangesAsync();

            return employee;
        }
    }
}
