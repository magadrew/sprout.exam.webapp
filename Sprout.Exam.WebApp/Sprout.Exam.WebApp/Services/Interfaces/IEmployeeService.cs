using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services.Interfaces
{
    public interface IEmployeeService
    {
        decimal CalculateRegularSalary(int id, decimal monthlySalary, decimal absentDays);

        decimal CalculateContractualSalary(int id, decimal dailyRate, decimal workedDays);

        Task<IEnumerable<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> CreateEmployee(CreateEmployeeDto employee);
        Task<Employee> EditEmployee(EditEmployeeDto employee);
        Task<Employee> RemoveEmployee(int id);
    }
}
