using Diplom.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Diplom
{
    public partial class AddEmployeeWindow : Window
    {
        private AppDbContext _context = new AppDbContext();

        Employee? _editingEmployee = null;

        public AddEmployeeWindow(Employee? employee = null)
        {
            InitializeComponent();

            // Если передан сотрудник, то мы редактируем уже существующего
            if (employee == null) return;

            // Заполняем поля данными существующего сотрудника
            TxtFullName.Text = employee.FullName;
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
        }

        private void SaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee newEmployee;

            // Если редактируем сотрудника
            if (_editingEmployee != null)
            {
                // находим существующего сотрудника в базе данных
                newEmployee = _context.Employees.FirstOrDefault(c => c.EmployeeID == _editingEmployee!.EmployeeID);
                if (newEmployee == null)
                {
                    MessageBox.Show("Служащий не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            // Если создаём нового сотрудника
            else
            {
                newEmployee = new Employee();
            }

            try
            {
                // Запоняем поля сотрудника данными, полученными от пользователя
                newEmployee.NumberPhone = TxtPhone.Text.Trim();
                newEmployee.Adress = TxtAddress.Text.Trim();
                newEmployee.Email = TxtEmail.Text.Trim();
                newEmployee.NumberPhone = TxtPhone.Text.Trim();
                newEmployee.FullName = TxtFullName.Text.Trim();
                newEmployee.PassportSeries = TxtPassportSeries.Text.Trim();
                newEmployee.PassportNumber = TxtPassportNumber.Text.Trim();
                newEmployee.PassportGivenBy = TxtPassportIssuedBy.Text.Trim();
                newEmployee.PassportGivenDateGivenBy = DpPassportDate?.SelectedDate;
                newEmployee.Registration = TxtRegistration.Text.Trim();
                newEmployee.INN = TxtINN.Text.Trim();
                newEmployee.SNILS = TxtSNILS.Text.Trim();
                newEmployee.Post = TxtPost.Text.Trim();

                // добавляем или обновляем сотрудника в базе данных
                _context.Employees.Add(newEmployee);
                _context.SaveChanges();

                _context.Entry(newEmployee).Reload();

                MessageBox.Show("Сотрудник сохранён", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

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
                    // Если произошла ошибка при добавлении нового сотрудника, удаляем его из базы данных
                    _context.Employees.Remove(newEmployee);
                }
            }
        }
    }
}