using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Services.Interfaces;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> Get()
        {
            var result = await _employeeService.GetEmployees();
            if (result != null)
                return Ok(result);
            return BadRequest();
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _employeeService.GetEmployeeById(id);
            if(result != null)
                return Ok(result);
            return BadRequest();
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(EditEmployeeDto input)
        {
            if (input.EmployeeTypeId == 0)
            {
                input.EmployeeTypeId = 1;
            }

            var item = await _employeeService.EditEmployee(input);

            if (item == null) return NotFound();
            item.FullName = input.FullName;
            item.Tin = input.Tin;
            item.Birthdate = input.Birthdate;
            item.EmployeeTypeId = input.EmployeeTypeId;
            return Ok(item);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            var result = await _employeeService.CreateEmployee(input);
            if (result != null)
                return Created($"/api/employees/{result.Id}", result.Id);
            return BadRequest();
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _employeeService.RemoveEmployee(id);

            if (result != null)
                return Ok(result);
            return NotFound();
        }



        /// <summary>
        /// Refactor this method to go through proper layers and use Factory pattern
        /// </summary>
        /// <param name="id"></param>
        /// <param name="absentDays"></param>
        /// <param name="workedDays"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(EmployeeAttendanceDto attendanceDto)
        {
            //var result = await Task.FromResult(StaticEmployees.ResultList.FirstOrDefault(m => m.Id == attendanceDto.Id));
            var result = await _employeeService.GetEmployeeById(attendanceDto.Id);

            if (result == null) return NotFound();
            var type = (EmployeeType) result.EmployeeTypeId;
            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(_employeeService.CalculateRegularSalary(attendanceDto.Id, 20000, attendanceDto.AbsentDays)),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(_employeeService.CalculateContractualSalary(attendanceDto.Id, 500, attendanceDto.WorkedDays)),
                _ => NotFound("Employee Type not found")
            };

        }

    }
}
