using Diplom;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Diplom.Models
{
    [Table("Employee")]
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; } = null!;
        public string NumberPhone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Adress { get; set; } = null!;
        public string PassportSeries { get; set; } = null!;
        public string PassportNumber { get; set; } = null!;
        public string PassportGivenBy { get; set; } = null!;
        public DateTime? PassportGivenDateGivenBy { get; set; } = DateTime.Now;
        public DateTime? EmployeeBirthDate {  get; set; } = DateTime.Now;
        public string Registration { get; set; } = null!;
        public string INN { get; set; } = null!;
        public string SNILS { get; set; } = null!;
        public string? Post { get; set; }

        // Навигационное свойство
        public ICollection<Connection> Connections { get; set; } = new List<Connection>();
    }

}
