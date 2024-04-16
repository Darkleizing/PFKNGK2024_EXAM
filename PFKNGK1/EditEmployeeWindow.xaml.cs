using PFKNGK1.AppData;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace PFKNGK1
{
    /// <summary>
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();
        private int _employeeId;

        public EditEmployeeWindow(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            LoadEmployeeData();
        }

        private void LoadEmployeeData()
        {
            // Загрузка информации о сотруднике по его ID
            var employee = _dbContext.Сотрудники
                .Where(c => c.ID_сотрудника == _employeeId)
                .Select(c => new
                {
                    ID_Сотрудника = c.ID_сотрудника,
                    ФИО = c.Имя,
                    Отдел = c.Отдел,
                    Должность = c.Фамилия,
                })
                .FirstOrDefault();

            if (employee != null)
            {
                // Заполнение полей формы информацией о сотруднике
                FullNameTextBox.Text = employee.ФИО;
                DepartmentTextBox.Text = employee.Отдел;
                PositionTextBox.Text = employee.Должность;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение изменений информации о сотруднике
            var employee = _dbContext.Сотрудники.Find(_employeeId);
            if (employee != null)
            {
                employee.Имя = FullNameTextBox.Text;
                employee.Отдел = DepartmentTextBox.Text;
                employee.Фамилия = PositionTextBox.Text;
                _dbContext.SaveChanges(); // Сохранение изменений в базе данных
            }
            this.Close(); // Закрытие окна после сохранения
        }
    }
}
