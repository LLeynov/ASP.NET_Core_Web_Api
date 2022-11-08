using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    [Table("Employees")]
    public class Employee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey(nameof(EmployeeType))]
        public int EmployeeTypeId { get; set; }

        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }

        [Column]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Column]
        [StringLength(255)]
        public string Surname { get; set; }

        [Column] 
        [StringLength(255)] 
        public string Patronymic { get; set; }

        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public Department Department { get; set; }

    }
}
