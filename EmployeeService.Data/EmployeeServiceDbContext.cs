using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Data
{
    public class EmployeeServiceDbContext : DbContext
    {
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        public DbSet<AccountSessions> AccountSessions { get; set; }

        public EmployeeServiceDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
