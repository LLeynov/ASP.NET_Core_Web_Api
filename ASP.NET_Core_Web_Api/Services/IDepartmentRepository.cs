using ASP.NET_Core_Web_Api.Models;
using EmployeeService.Data;

namespace ASP.NET_Core_Web_Api.Services
{
    public interface IDepartmentRepository : IRepository<Department,int>
    {
    }
}
