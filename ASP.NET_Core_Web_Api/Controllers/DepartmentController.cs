using ASP.NET_Core_Web_Api.Models.Requests;
using ASP.NET_Core_Web_Api.Services;
using ASP.NET_Core_Web_Api.Services.Impl;
using EmployeeService.Data;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_Web_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        [HttpPost("department/create")]
        public ActionResult<int> CreateDepartment([FromQuery] string description)
        {
            return Ok(_departmentRepository.Create(new Department
            {
                Description = description
            }));
        }


        [HttpGet("departments/all")]
        public ActionResult<IList<DepartmentDto>> GetAllDepartments()
        {
            return Ok(_departmentRepository.GetAll().Select(et =>
                new EmployeeTypeDto
                {
                    Id = et.Id,
                    Description = et.Description
                }
            ).ToList());
        }
       
        [HttpDelete("department/delete")]
        public ActionResult<bool> DeleteDepartment([FromQuery] int id)
        {
            return Ok(_departmentRepository.Delete(id));
        }
    }
}
