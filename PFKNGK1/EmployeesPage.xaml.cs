using PFKNGK1.AppData;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PFKNGK1
{
    /// <summary>
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();

        public EmployeesPage()
        {
            InitializeComponent();
            // Загрузка данных о сотрудниках при открытии страницы
            LoadEmployee();


        }

        private void EmployeeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = EmployeeDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddEmployeeButton.IsEnabled = true; // Добавление всегда доступно
            DeleteEmployeeButton.IsEnabled = isRowSelected;
            EditEmployeeButton.IsEnabled = isRowSelected;
        }

        private void LoadEmployee()
        {
            var employee = _dbContext.Сотрудники
                .Select(c => new {
                    ID_Сотрудника = c.ID_сотрудника,
                    ФИО = c.Имя,
                    Отдел = c.Отдел,
                    Должность = c.Фамилия,
                })
                .ToList();

            EmployeeDataGrid.ItemsSource = employee;
        }

        private void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления заказчика
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.ShowDialog(); // Отображение окна как модального
            LoadEmployee(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedEmployee = EmployeeDataGrid.SelectedItem;

                    int employeeId = selectedEmployee.ID_Сотрудника; // Получение ID выбранного сотрудника

                    var employeeToDelete = _dbContext.Сотрудники.Find(employeeId);

                    if (employeeToDelete != null)
                    {
                        _dbContext.Сотрудники.Remove(employeeToDelete); // Удаление сотрудника из БД
                        _dbContext.SaveChanges(); // Сохранение изменений

                        LoadEmployee(); // Перезагрузка данных после удаления
                    }
                }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.InnerException != null)
                {
                    MessageBox.Show($"Ошибка удаления сотрудника: {ex.InnerException.InnerException.Message}");
                }
                else
                {
                        MessageBox.Show($"Ошибка удаления сотрудника: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления сотрудника: {ex.Message}");
                }
            }
        }

        private void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeDataGrid.SelectedItem != null)
            {
                dynamic selectedEmployee = EmployeeDataGrid.SelectedItem;
                int employeeId = selectedEmployee.ID_Сотрудника; // Получение ID выбранного сотрудника

                // Создание нового окна для редактирования сотрудника
                EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(employeeId);
                editEmployeeWindow.ShowDialog(); // Отображение окна как модального
                LoadEmployee(); // Перезагрузка данных после закрытия окна редактирования
            }
        }


    }
}
