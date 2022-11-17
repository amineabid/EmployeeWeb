﻿using EmployeeWebApi.Models;
using EmployeeWebApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static EmployeeWebApi.Models.Department;

namespace EmployeeWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //Injection de dépendance du service IEmployeeRepository
        private readonly IEmployeeRepository employeeRepository;
        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetEmployees()
        {
            try
            {
                return Ok(await employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await employeeRepository.GetEmployee(id);
                if (result == null) return NotFound();
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error retrieving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                var createdEmployee = await employeeRepository.AddEmployee(employee);

                return CreatedAtAction(nameof(GetEmployee),
                new { id = createdEmployee.EmployeeId }, createdEmployee);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Employee>> UpdateEmployee(int id, Employee employee)
        {
            try
            {
                if (id != employee.EmployeeId)
                    return BadRequest();
                var employeeToUpdate = await employeeRepository.GetEmployee(id);
                if (employeeToUpdate == null)
                    return NotFound($"Employee with id = {id} not found");
                return await employeeRepository.UpdateEmployee(employee);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data");
            }
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employeeToDelete = await employeeRepository.GetEmployee(id);

                if (employeeToDelete == null)
                    return NotFound($"Employee with id = {id} not found");
                
                return await employeeRepository.DeleteEmployee(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data");
            }
        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> Search(string name, Gender? gender)
        {
            try
            {
                var result = await employeeRepository.Search(name, gender);

                if (result.Any())
                    return Ok(result);

                return NotFound(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database");
            }
        }
    }
}
