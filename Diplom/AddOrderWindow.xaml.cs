using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Windows;
using Diplom.Models;

namespace Diplom
{
    public partial class AddOrderWindow : Window
    {
        private readonly AppDbContext _context;
        private readonly Order _editingOrder;
        // Текущий выбранный сотрудник
        private Employee _selectedEmployee;
        private readonly bool _isEditMode;

        public AddOrderWindow(AppDbContext context, Order? order = null)
        {
            InitializeComponent();
            _context = context;
            _editingOrder = order ?? new Order();
            _isEditMode = order != null;

            LoadEmployees();
            InitForm();
        }

        private void LoadEmployees()
        {
            CmbEmployees.ItemsSource = _context.Employees.ToList();
        }

        private void InitForm()
        {
            if (_isEditMode)
            {
                TxtContractNumber.Text = _editingOrder.OrderID.ToString();
                TxtOrderName.Text = _editingOrder.OrderName;
                TxtNotes.Text = _editingOrder.OrderDescription;

                var connection = _context.Connections.FirstOrDefault(c => c.OrderID == _editingOrder.OrderID);
                if (connection != null)
                {
                    CmbEmployees.SelectedValue = connection.EmployeeID;
                    IsDoneChk.IsChecked = connection.OrderStatus;
                }
            }
            else
            {
                TxtContractNumber.Text = GenerateNewOrderId().ToString();
                DpStartDate.SelectedDate = DateTime.Today;
                DpEndDate.SelectedDate = DateTime.Today.AddDays(7);
            }
        }

        private int GenerateNewOrderId()
        {
            return _context.Orders.Any() ? _context.Orders.Max(o => o.OrderID) + 1 : 1;
        }

        private void BtnSaveContract_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, что поля заполнены
            if (string.IsNullOrWhiteSpace(TxtOrderName.Text))
            {
                MessageBox.Show("Название заказа обязательно.", 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning);
                return;
            }

            if (CmbEmployees.SelectedValue == null)
            {
                MessageBox.Show("Выберите исполнителя.", 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning);
                return;
            }

            // Получаем ID выбранного исполнителя
            int employeeId = (int)CmbEmployees.SelectedValue;

            _editingOrder.OrderName = TxtOrderName.Text.Trim();
            _editingOrder.OrderDescription = TxtNotes.Text.Trim();

            int orderId;

            try
            {
                // Если редактируем существующий заказ
                if (_isEditMode)
                {
                    orderId = _editingOrder.OrderID;
                    _context.Orders.Update(_editingOrder);

                    // Обновляем связь с исполнителем
                    var existingConnection = _context.Connections.FirstOrDefault(c => c.OrderID == orderId);
                    if (existingConnection != null)
                    {
                        existingConnection.EmployeeID = employeeId;
                        existingConnection.OrderStatus = IsDoneChk.IsChecked == true;
                        _context.Connections.Update(existingConnection);
                    }
                }
                // Если создаём новый заказ
                else
                {
                    _context.Orders.Add(_editingOrder);
                    _context.SaveChanges();

                    orderId = _editingOrder.OrderID;

                    // Создаём связь с исполнителем
                    _context.Connections.Add(new Connection
                    {
                        OrderID = orderId,
                        EmployeeID = employeeId,
                        OrderStatus = IsDoneChk.IsChecked == true,
                    });
                }

                // Сохраняем изменения в базе данных
                _context.SaveChanges();
                // Создаём документ заказа
                CreateActDocument(_editingOrder);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}: {ex.InnerException}", 
                                "Ошибка при сохранении", 
                                MessageBoxButton.OK, 
                                MessageBoxImage.Error);
            }

            DialogResult = true;
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        // Кнопка "Создать нового сотрудника"
        private void BtnNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Открываем окно для создания нового сотрудника
            AddEmployeeWindow window = new AddEmployeeWindow();
            // Блокируем основное окно, пока открыто новое
            this.IsEnabled = false;

            // Если окно успешно закрыто (пользователь сохранил нового сотрудника),
            // то обновляем список сотрудников
            if (window.ShowDialog() == true)
            {
                LoadEmployees();
                // Выбираем последнего добавленного сотрудника
                CmbEmployees.SelectedItem = _context.Employees.OrderBy(c => c.EmployeeID).Last();
            }
            // Разблокируем основное окно
            this.IsEnabled = true;
        }

        private void CmbEmployees_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (CmbEmployees != null)
            {
                try
                {
                    _selectedEmployee = CmbEmployees.SelectedItem as Employee;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + " " + ex.InnerException, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public bool CreateActDocument(Models.Order Order)
        {
            bool successfullyCreated = true;
            try
            {
                // Путь к шаблону документа
                string templateOrderPath = "./Templates/OrderTemplate.docx";
                // Проверяем, существует ли файл шаблона
                string? ordersPath = ConfigurationManager.AppSettings["OrdersFolderPath"];
                if (string.IsNullOrWhiteSpace(ordersPath))
                {
                    MessageBox.Show("Не удалось создать заказ: в файле с конфигом не указан путь", 
                                    "Ошибка", 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Error);
                    return !successfullyCreated;
                }

                // Путь для нового файла заказа
                string outputOrderPath = System.IO.Path.Combine(ordersPath, $"Заказ_№{Order.OrderID}.docx");
                
                // Создаём папку, если её нет
                if (!Directory.Exists(ordersPath))
                {
                    Directory.CreateDirectory(ordersPath);
                }

                // Копируем шаблон, чтобы не изменять оригинальный файл
                File.Copy(templateOrderPath, outputOrderPath, true);

                // Получаем связь между заказом и исполнителем
                var connection = _context.Connections.FirstOrDefault(c => c.OrderID == Order.OrderID);

                // Словарь замен для заполнения шаблона (тег и значение для замены)
                Dictionary<string, string> replacements;
                replacements = new Dictionary<string, string>
                {
                    { "#Date#", DateTime.Today.ToLongDateString()},
                    { "#OrderID#", Order.OrderID.ToString() },
                    { "#OrderName#", Order.OrderName.ToString() },
                    { "#EmployeeName#", connection.Employee.FullName },
                    { "#OrderDescription#", Order.OrderDescription },
                };

                // Заменяем теги в документе
                WordProcessor.ReplaceTags(outputOrderPath, replacements);

                MessageBox.Show($"Заказ успешно создан: {outputOrderPath}", 
                                    "Готово", 
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Information);

                return successfullyCreated;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка при создании акта: " + ex.Message + ex.InnerException, 
                                    "Ошибка",
                                    MessageBoxButton.OK, 
                                    MessageBoxImage.Error);
                return !successfullyCreated;
            }
        }
    }
}
