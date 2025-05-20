using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Diplom
{
    public partial class AddEmployeeWindow : Window
    {
        private AppDbContext? _context;

        Employee? _editingEmployee = null;

        public AddEmployeeWindow(Employee? employee = null, AppDbContext? context = null)
        {
            InitializeComponent();

            // Если передан сотрудник, то мы редактируем уже существующего
            if (employee == null)
            {
                _context = new AppDbContext();
            }

            // Заполняем поля данными существующего сотрудника
            TxtFullName.Text = employee.FullName;
            DpBirthDate.SelectedDate = employee.EmployeeBirthDate ?? DateTime.Today;
            TxtPassportSeries.Text = employee.PassportSeries;
            TxtPassportNumber.Text = employee.PassportNumber;
            TxtPassportIssuedBy.Text = employee.PassportGivenBy;
            DpPassportDate.SelectedDate = employee.PassportGivenDateGivenBy;
            TxtPhone.Text = employee.NumberPhone;
            TxtEmail.Text = employee.Email;
            TxtAddress.Text = employee.Adress;
            TxtRegistration.Text = employee.Registration;
            TxtINN.Text = employee.INN;
            TxtSNILS.Text = employee.SNILS;
            TxtPost.Text = employee.Post;

            _editingEmployee = employee;
            _context = context;
        }

        private void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee;

            if (_editingEmployee != null)
            {
                employee = _context.Employees.FirstOrDefault(c => c.EmployeeID == _editingEmployee.EmployeeID);
                if (employee == null)
                {
                    MessageBox.Show("Служащий не найден", 
                                    "Ошибка", 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                employee = new Employee();
                _context.Employees.Add(employee);
            }

            try
            {
                // Заполняем поля
                employee.FullName = TxtFullName.Text.Trim();
                employee.EmployeeBirthDate = DpBirthDate.SelectedDate ?? DateTime.Today;
                employee.PassportSeries = TxtPassportSeries.Text.Trim();
                employee.PassportNumber = TxtPassportNumber.Text.Trim();
                employee.PassportGivenBy = TxtPassportIssuedBy.Text.Trim();
                employee.PassportGivenDateGivenBy = DpPassportDate?.SelectedDate;
                employee.NumberPhone = TxtPhone.Text.Trim();
                employee.Email = TxtEmail.Text.Trim();
                employee.Adress = TxtAddress.Text.Trim();
                employee.Registration = TxtRegistration.Text.Trim();
                employee.INN = TxtINN.Text.Trim();
                employee.SNILS = TxtSNILS.Text.Trim();
                employee.Post = TxtPost.Text.Trim();

                _context.SaveChanges();

                MessageBox.Show("Сотрудник сохранён",
                                "Успех", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Information);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message} {ex.InnerException}",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                if (_editingEmployee == null)
                {
                    _context.Employees.Remove(employee);
                }
            }
        }
    }
}