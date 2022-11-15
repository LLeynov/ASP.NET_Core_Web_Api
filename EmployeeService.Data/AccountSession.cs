using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    [Table("AccountSessions")]
    public class AccountSession
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SessionId { get; set; }

        [Required]
        [StringLength(384)]
        public string SessionToken { get; set; }

        [ForeignKey(nameof(Account))]
        public int AccountId { get; set; }

        public bool IsClosed { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TimeCreated { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? TimeClosed { get; set; }

        public virtual Account Account { get; set; }
    }
}
