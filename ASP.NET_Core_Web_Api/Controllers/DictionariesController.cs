using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionariesController : ControllerBase
    {
        private readonly IEmployeeTypeRepository _employeeTypeRepository;

        public DictionariesController(IEmployeeTypeRepository employeeTypeRepository)
        {
            _employeeTypeRepository = employeeTypeRepository;
        }
       
        [HttpPost("employee-type/create")]
        public IActionResult Create([FromBody] CreateEmployeeTypeRequest request)
        {
            return Ok(_employeeTypeRepository.Create(new Models.EmployeeType
            {
                Description = request.Description,
            }));
        }

        [HttpGet("employee-types/all")]
        public IActionResult GetAll()
        {
            return Ok(_employeeTypeRepository.GetAll());
        }

        [HttpGet("employee-type-by-id/{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            return Ok(_employeeTypeRepository.GetById(id));
        }
    }
}
