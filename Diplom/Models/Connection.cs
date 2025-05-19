using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Diplom.Models
{
    [Table("Connection")]
    public class Connection
    {
        [ForeignKey(nameof(Order))]
        public int OrderID { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }
        public bool OrderStatus { get; set; }

        // Навигационные свойства
        public Order Order { get; set; } = null!;
        public Employee Employee { get; set; } = null!;
    }
}
