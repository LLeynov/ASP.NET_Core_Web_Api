using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_Api.Controllers
{
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
        public IActionResult Create([FromBody] CreateEmployeeRequest request)
        {
            return Ok(_employeeRepository.Create(new Models.Employee
            {
                DepartmentId = request.DepartmentId,
                EmployeeTypeId = request.EmployeeTypeId,
                FirstName = request.FirstName,
                Patronymic = request.Patronymic,
                Salary = request.Salary,
                Surname = request.Surname
            }));
        }

        [HttpGet("employee/all")]
        public IActionResult GetAllEmployees()
        {
            return Ok(_employeeRepository.GetAll());
        }
        
        [HttpGet("employee-by-id/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_employeeRepository.GetById(id));
        }
    }
}
