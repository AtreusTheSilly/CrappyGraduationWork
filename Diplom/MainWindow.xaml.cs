using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Diplom.Models;
using Microsoft.EntityFrameworkCore;

namespace Diplom
{
    public partial class MainWindow : Window
    {
        private AppDbContext _context;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show("Открывается", "ОТКРОЙСЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
                _context = new AppDbContext();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void LoadData(string? search = null)
        {
            try
            {
                // Загрузка данных из базы данных
                var data = _context.Connections
                    .Include(c => c.Order)
                    .Include(c => c.Employee)
                    .Select(c => new
                    {
                        c.OrderID,
                        c.Order.OrderName,
                        c.Order.OrderDescription,
                        c.OrderStatus,
                        c.EmployeeID,
                        EmployeeName = c.Employee.FullName,
                    });

                // Фильтрация
                if (!string.IsNullOrWhiteSpace(search))
                {
                    if (SearchTypeComboBox.SelectedIndex == 0)
                    {
                        // фильтруем по имени сотрудника
                        data = data.Where(d => d.OrderName.Contains(search));
                    }
                    else
                    {
                        // фильтруем по названию заказа
                        data = data.Where(d => d.EmployeeName.Contains(search));
                    }
                }

                // Обновление главной таблицы
                MainDataGrid.ItemsSource = data.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Поиск элемента при изменении текста в поле для ввода искомого значения
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (SearchTextBox == null)
                    return;

                LoadData(SearchTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Поиск элемента при изменении типа поиска
        private void SearchTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SearchTextBox == null)
                    return;

                LoadData(SearchTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void MainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Проверяем, что элемент выбран
                var selectedItem = MainDataGrid.SelectedItem;
                if (selectedItem == null)
                {
                    NotesTextBox.Text = "";
                    return;
                }

                // Из выбранного анонимного объекта получаем OrderID
                var prop = selectedItem.GetType().GetProperty("OrderID");
                if (prop == null)
                    return;

                // Получаем ID выбранного заказа
                int OrderID = (int)prop.GetValue(selectedItem);
                var order = _context.Orders.FirstOrDefault(o => o.OrderID == OrderID);
                if (order == null)
                    return;

                // Отображаем примечания (если поле Notes равно null – выводим пустую строку)
                NotesTextBox.Text = order.OrderDescription ?? "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void BtnCreateContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // открытие окна добавления заказа
                AddOrderWindow window = new AddOrderWindow(_context);
                // блокировка главного окна (текущего)
                this.IsEnabled = false;
                // если окно добавления заказа закрылось с результатом true (нажали "Сохранить"),
                // то обновляем данные в главном окне
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
                // разблокировка главного окна
                this.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // удаление заказа
        private void BtnDeleteContract_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // проверка, что элемент выбран
                var selectedItem = MainDataGrid.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Выберите заказ для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // из выбранного анонимного объекта получаем свойство OrderID
                var prop = selectedItem.GetType().GetProperty("OrderID");
                if (prop == null)
                    return;

                // находим Order в базе данных
                int OrderID = (int)prop.GetValue(selectedItem);
                var order = _context.Orders.FirstOrDefault(o => o.OrderID == OrderID);
                if (order == null)
                {
                    MessageBox.Show("Заказ не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Запрос подтверждения у пользователя
                if (MessageBox.Show("Вы действительно хотите удалить выбранный заказ?", 
                                    "Подтверждение", 
                                    MessageBoxButton.YesNo, 
                                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    // Удаление заказа и связанных с ним данных
                    _context.Orders.Remove(order);

                    var connections = _context.Connections.Where(c => c.OrderID == OrderID);
                    _context.Connections.RemoveRange(connections);

                    // сохранение изменений в базе данных
                    _context.SaveChanges();
                    MessageBox.Show("Заказ успешно удалён.", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);

                    // обновление данных в главном окне
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // Создание нового сотрудника
        private void BtnCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddEmployeeWindow window = new AddEmployeeWindow();
                this.IsEnabled = false;
                if (window.ShowDialog() == true)
                {
                    LoadData();
                }
                this.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // удаление сотрудника
        private void BtnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedItem = MainDataGrid.SelectedItem;
                if (selectedItem == null)
                {
                    MessageBox.Show("Выберите сотрудника для удаления.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var prop = selectedItem.GetType().GetProperty("EmployeeID");
                if (prop == null)
                    return;

                int EmployeeID = (int)prop.GetValue(selectedItem);
                var employee = _context.Employees.FirstOrDefault(e => e.EmployeeID == EmployeeID);
                if (employee == null)
                {
                    MessageBox.Show("Сотрудник не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                var connections = _context.Connections.Where(c => c.EmployeeID == EmployeeID);
                _context.Connections.RemoveRange(connections);

                _context.Employees.Remove(employee);
                _context.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        // ПКМ — открытие файла с заказом
        private void MenuItem_OpenOrder_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что элемент выбран
            var selectedItem = MainDataGrid.SelectedItem;
            if (selectedItem == null) return;

            // Получаем ID выбранного договора
            var prop = selectedItem.GetType().GetProperty("OrderID");
            if (prop == null) return;

            // Получаем ID выбранного заказа
            int OrderID = (int)prop.GetValue(selectedItem);

            // Получаем путь к файлу из config-файла
            string? basePath = ConfigurationManager.AppSettings["OrdersFolderPath"];
            if (string.IsNullOrWhiteSpace(basePath))
            {
                MessageBox.Show("В файле с конфигом не указан путь к папке с заказами", 
                                "Ошибка", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Error);
                return;
            }

            // Формируем путь к заказу, объединяя указанный в конфиге путь и имя файла, в котором содержится ID заказа
            string filePath = System.IO.Path.Combine(basePath, $@"Заказ_№{OrderID.ToString()}.docx");
            try
            {
                // Попытка открыть указанный файл
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true // Использование ассоциированного приложения (Word, LibreOffice и т.д.)
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при открытии файла: " + ex.Message + ex.InnerException, 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return;
            }
        }

        private void MainDataGrid_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                // Проверяем, что клик был двойным и по левой кнопке мыши
                if (e.ChangedButton != MouseButton.Left) return;

                // Проверяем, что элемент выбран
                var selectedItem = MainDataGrid.SelectedItem;
                if (selectedItem == null) return;

                var selectedItemId = MainDataGrid.SelectedIndex;

                // Получаем ID выбранного договора
                var prop = selectedItem.GetType().GetProperty("OrderID");
                if (prop == null) return;

                // находим Order в базе данных
                int OrderID = (int)prop.GetValue(selectedItem);
                var order = _context.Orders.FirstOrDefault(o => o.OrderID == OrderID);
                if (order == null)
                {
                    MessageBox.Show("Заказ не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // открытие окна редактирования заказа
                AddOrderWindow editWindow = new AddOrderWindow(_context, order);
                // блокировка главного окна (текущего)
                this.IsEnabled = false;
                // обновление списка
                if (editWindow.ShowDialog() == true)
                {
                    _context.Entry(order).Reload();
                    LoadData();

                    MainDataGrid.UnselectAll();
                    MainDataGrid.SelectedItem = null;
                }
                // разблокировка главного окна
                this.IsEnabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
    }
}
