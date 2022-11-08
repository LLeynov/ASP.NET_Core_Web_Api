using ASP.NET_Core_Web_Api.Models;
using EmployeeService.Data;

namespace ASP.NET_Core_Web_Api.Services.Impl
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeServiceDbContext _dbContext;


        public DepartmentRepository(EmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Department> GetAll()
        {
            return _dbContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
           return _dbContext.Departments.FirstOrDefault(et => et.Id == id);
        }

        public int Create(Department data)
        {
            _dbContext.Departments.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

        public bool Update(Department data)
        {
            Department department = GetById(data.Id);
            if (department != null)
            {
                _dbContext.Departments.Update(department);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            Department department = GetById(id);
            if (department != null)
            {
                _dbContext.Departments.Remove(department);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
