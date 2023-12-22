using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(CreateEmployeeDto employeeDto);
        Task<Employee> EditEmployee(EditEmployeeDto employee);
        Task<Employee> RemoveEmployee(int id);
    }
}
