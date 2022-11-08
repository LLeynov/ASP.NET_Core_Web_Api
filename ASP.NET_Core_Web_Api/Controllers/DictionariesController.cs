using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using EmployeeService.Data;
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

        [HttpGet("employee-types/all")]
        public ActionResult<IList<EmployeeTypeDto>> GetAllEmployeeTypes()
        {
            return Ok(_employeeTypeRepository.GetAll().Select(et =>
                new EmployeeTypeDto
                {
                    Id = et.Id,
                    Description = et.Description
                }
            ).ToList());
        }


        [HttpPost("employee-types/create")]
        public ActionResult<int> CreateEmployeeType([FromQuery] string description)
        {
            return Ok(_employeeTypeRepository.Create(new EmployeeType
            {
                Description = description
            }));
        }

        [HttpDelete("employee-types/delete")]
        public ActionResult<bool> DeleteEmployeeType([FromQuery] int id)
        {
            return Ok(_employeeTypeRepository.Delete(id));
        }
    }
}
