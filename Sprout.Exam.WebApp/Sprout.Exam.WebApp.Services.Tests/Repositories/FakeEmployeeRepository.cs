using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services.Tests.Repositories
{
    public class FakeEmployeeRepository : IEmployeeRepository
    {
        public bool GetEmployeeByIdCalled { get; private set; }
        public int GetEmployeeById_Id { get; private set; }
        public bool CreateEmployeeCalled { get; private set; }

        public Task<Employee> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            Employee createEmployee = new Employee();

            createEmployee.FullName = employeeDto.FullName;
            CreateEmployeeCalled = true;
            return Task.FromResult(createEmployee);
        }

        public bool EditEmployeeCalled { get; private set; }

        public Task<Employee> EditEmployee(EditEmployeeDto employee)
        {
            Employee editedEmployee = new Employee();

            editedEmployee.Id = employee.Id;
            editedEmployee.FullName = employee.FullName;
            EditEmployeeCalled = true;
            return Task.FromResult(editedEmployee);
        }

        public Task<Employee> GetEmployeeById(int id)
        {
            GetEmployeeByIdCalled = true;
            GetEmployeeById_Id = 1;

            if (id == 1)
                return Task.FromResult(new Employee() { Id = 1, FullName = "Lebron James" });
            return Task.FromResult<Employee>(null);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            var emp1 = new Employee() { Id = 1, FullName = "Lebron James" };
            var emp2 = new Employee() { Id = 1, FullName = "Michael Jordan" };

            List<Employee> employees = new List<Employee>();

            employees.Add(emp1);
            employees.Add(emp2);

            List<Employee> result = await Task.FromResult(employees);
            return result;
        }

        public bool RemoveEmployeeCalled { get; private set; }

        public Task<Employee> RemoveEmployee(int id)
        {
            Employee removeEmployee = new Employee();

            removeEmployee.Id = id;
            RemoveEmployeeCalled = true;
            return Task.FromResult(removeEmployee);
        }
    }
}
