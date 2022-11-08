using System.Collections;
using ASP.NET_Core_Web_Api.Models;
using EmployeeService.Data;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_Web_Api.Services.Impl
{
    public class EmployeeTypeRepository : IEmployeeTypeRepository
    {
        private readonly EmployeeServiceDbContext _dbContext;
       
        
        public EmployeeTypeRepository(EmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IList<EmployeeType> GetAll()
        {
            return _dbContext.EmployeeTypes.ToList();
        }

        public EmployeeType GetById(int id)
        {
            return _dbContext.EmployeeTypes.FirstOrDefault(et => et.Id == id);
        }

        public int Create(EmployeeType data)
        {
            _dbContext.EmployeeTypes.Add(data);
            _dbContext.SaveChanges();
            return data.Id;
        }

        public bool Update(EmployeeType data)
        {
            EmployeeType employeeType = GetById(data.Id);
            if (employeeType != null)
            {
                _dbContext.EmployeeTypes.Update(employeeType);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            EmployeeType employeeType = GetById(id);
            if (employeeType != null)
            {
                _dbContext.EmployeeTypes.Remove(employeeType);
                _dbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
