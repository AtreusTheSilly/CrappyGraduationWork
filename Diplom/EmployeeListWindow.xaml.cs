using System.Linq;
using System.Windows;
using System.Windows.Input;
using Diplom.Models;

namespace Diplom
{
    public partial class EmployeeListWindow : Window
    {
        private readonly AppDbContext _context;

        public EmployeeListWindow()
        {
            InitializeComponent();
            _context = new AppDbContext();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeeDataGrid.ItemsSource = _context.Employees.ToList();
        }

        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                AddEmployeeWindow editWindow = new AddEmployeeWindow(selectedEmployee);
                this.IsEnabled = false;
                if (editWindow.ShowDialog() == true)
                {
                    LoadEmployees(); // обновляем после редактирования
                }
                this.IsEnabled = true;
            }
        }
    }
}
