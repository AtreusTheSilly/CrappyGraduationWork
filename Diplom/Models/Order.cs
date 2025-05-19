using Diplom;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    [Table("Order")]
    public class Order
    {
        public int OrderID { get; set; }
        public string OrderName { get; set; } = null!;
        public string OrderDescription { get; set; } = null!;
        public DateTime OrderDateStart { get; set; }
        public DateTime OrderDateFinish { get; set; }

        // Навигационное свойство
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }

}
