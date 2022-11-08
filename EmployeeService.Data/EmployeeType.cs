using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    [Table("EmployeeTypes")]
    public class EmployeeType
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column]
        [StringLength(128)]
        public string Description { get; set; }

        [InverseProperty(nameof(Employee.EmployeeType))]
        public virtual ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}
