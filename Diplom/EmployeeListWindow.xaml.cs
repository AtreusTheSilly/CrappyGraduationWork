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
            try
            {
                EmployeeDataGrid.ItemsSource = _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void EmployeeDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem is Employee selectedEmployee)
            {
                AddEmployeeWindow editWindow = new AddEmployeeWindow(selectedEmployee, _context);
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
