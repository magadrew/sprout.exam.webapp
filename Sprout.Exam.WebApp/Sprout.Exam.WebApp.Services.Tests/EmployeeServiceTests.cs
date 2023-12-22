using NUnit.Framework;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.WebApp.Repositories.Interfaces;
using Sprout.Exam.WebApp.Services.Tests.Repositories;
using System.Threading.Tasks;

namespace Sprout.Exam.WebApp.Services.Tests
{
    public class EmployeeServiceTests
    {

        private static EmployeeService GetService(IEmployeeRepository repo)
        {
            return new EmployeeService(repo);
        }

        [TestCase(1)]
        public static async Task Get_Employee_By_Id_Valid(int id)
        {

            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            _ = await employeeService.GetEmployeeById(1);

            //Result
            Assert.IsTrue(employeeRepository.GetEmployeeByIdCalled);
            Assert.AreEqual(id, employeeRepository.GetEmployeeById_Id);
        }

        [TestCase()]
        public static async Task Get_Regular_Employee_Salary()
        {

            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var salary = employeeService.CalculateRegularSalary(1, 20000, 1);

            //Result
            Assert.AreEqual((double)salary, 16690.90, 0.1);
        }

        [TestCase()]
        public static async Task Get_Contractual_Employee_Salary()
        {

            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var salary = employeeService.CalculateContractualSalary(1, 500, 15.5m);

            //Result
            Assert.AreEqual(salary, 7750.00);
        }

        [TestCase()]
        public static async Task Update_Employee_Valid()
        {
            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var editedEmployee = new EditEmployeeDto()
            {
                FullName = "Lebronbron",
                Id = 1
            };

            var result = await employeeService.EditEmployee(editedEmployee);

            //Result
            Assert.IsTrue(employeeRepository.EditEmployeeCalled);
            Assert.AreEqual(editedEmployee.Id, result.Id);
        }

        [TestCase()]
        public static async Task Create_Employee_Valid()
        {
            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var createEmployee = new CreateEmployeeDto()
            {
                FullName = "Michael Jordan",
            };

            var result = await employeeService.CreateEmployee(createEmployee);

            //Result
            Assert.IsTrue(employeeRepository.CreateEmployeeCalled);
            Assert.AreEqual(createEmployee.FullName, result.FullName);
        }

        [TestCase()]
        public static async Task List_Employee_Valid()
        {
            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var result = await employeeService.GetEmployees();

            //Result
            Assert.IsNotEmpty(result);
        }

        [TestCase()]
        public static async Task Remove_Employee_Valid()
        {
            //Setup
            var employeeRepository = new FakeEmployeeRepository();
            var employeeService = GetService(employeeRepository);

            //Action
            var result = await employeeService.RemoveEmployee(1);

            //Result
            Assert.IsTrue(employeeRepository.RemoveEmployeeCalled);
            Assert.AreEqual(1, result.Id);
        }
    }
}