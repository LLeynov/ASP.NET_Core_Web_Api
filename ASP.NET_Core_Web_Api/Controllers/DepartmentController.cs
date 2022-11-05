using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _DepartmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _DepartmentRepository = departmentRepository;
        }

        [HttpPost("department/create")]
        public IActionResult Create([FromBody] CreateDepartmentRequest request)
        {
            return Ok(_DepartmentRepository.Create(new Models.Department
            {
               Description = request.Description,
            }));
        }


        [HttpGet("departments/all")]
        public IActionResult GetAllDepartments()
        {
            return Ok(_DepartmentRepository.GetAll());
        }
        [HttpGet("departments-by-id/{id}")]
        public IActionResult GetDepartmentById([FromRoute] Guid id)
        {
            return Ok(_DepartmentRepository.GetById(id));
        }
    }
}
