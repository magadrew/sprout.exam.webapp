using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Repositories;
using Sprout.Exam.WebApp.Repositories.Interfaces;
using Sprout.Exam.WebApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public decimal CalculateRegularSalary(int id, decimal monthlySalary, decimal absentDays)
        {
            decimal absentDeduction = monthlySalary / 22;

            decimal totalAbsentDeduction = absentDays * absentDeduction;

            decimal taxDeduction = monthlySalary * (decimal)0.12;

            decimal finalPay = monthlySalary - totalAbsentDeduction - taxDeduction;

            return finalPay;
        }

        public decimal CalculateContractualSalary(int id, decimal dailyRate, decimal workedDays)
        {
            decimal finalPay = dailyRate * workedDays;

            return finalPay;
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployees();
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _employeeRepository.GetEmployeeById(id);
        }

        public async Task<Employee> CreateEmployee(CreateEmployeeDto employeeDto)
        {
            return await _employeeRepository.CreateEmployee(employeeDto);
        }

        public async Task<Employee> EditEmployee(EditEmployeeDto employee)
        {
            return await _employeeRepository.EditEmployee(employee);
        }

        public async Task<Employee> RemoveEmployee(int id)
        {
            return await _employeeRepository.RemoveEmployee(id);
        }
    }
}
