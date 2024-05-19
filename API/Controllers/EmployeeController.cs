using API.Helpers;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Repository.IRepositories;
using Serilog;
using Service.DTO;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Util.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }


        [HttpGet]
        public async Task<IActionResult> GetEmployee([FromQuery] Guid id)
        {
            var result = await _employeeService.GetEmployee(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeDTO employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            return Ok(result);
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees([FromQuery] PaginationFilter Filter)
        {
            var result = await _employeeService.GetEmployees(Filter);
            return Ok(result);

        }
    }
}
