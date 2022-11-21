using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using Azure.Core;
using EmployeeService.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpPost("employee/create")]
        public ActionResult<int> Create([FromBody] EmployeeDto request)
        {
            return Ok(_employeeRepository.Create(new Employee
            {
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Patronymic = request.Patronymic,
                Salary = request.Salary,
                Surname = request.Surname
            }));
        }

        [HttpGet("employee/all")]
        public ActionResult<IList<EmployeeDto>> GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAll().Select(request =>
                new EmployeeDto
                {
                    EmployeeTypeId = request.EmployeeTypeId,
                    FirstName = request.FirstName,
                    Patronymic = request.Patronymic,
                    Salary = request.Salary,
                    Surname = request.Surname
                }
            ).ToList());
        }
        
        [HttpGet("employee/delete")]
        public ActionResult<bool> GetById([FromQuery] int id)
        {
            return Ok(_employeeRepository.GetById(id));
        }
    }
}
