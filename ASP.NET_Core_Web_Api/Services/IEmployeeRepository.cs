using ASP.NET_Core_Web_Api.Models;

namespace ASP.NET_Core_Web_Api.Services
{
    public interface IEmployeeRepository : IRepository<Employee,int>
    {
    }
}
