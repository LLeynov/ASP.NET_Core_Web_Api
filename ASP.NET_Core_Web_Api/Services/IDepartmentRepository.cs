using ASP.NET_Core_Web_Api.Models;

namespace ASP.NET_Core_Web_Api.Services
{
    public interface IDepartmentRepository : IRepository<Department,Guid>
    {
    }
}
