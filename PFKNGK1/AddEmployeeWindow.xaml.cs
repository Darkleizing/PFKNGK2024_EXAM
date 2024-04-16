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
    /// Логика взаимодействия для AddEmployeeWindow.xaml
    /// </summary>
    public partial class AddEmployeeWindow : Window
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();

        public AddEmployeeWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового сотрудника на основе введенных данных
            var newEmployee = new Сотрудники
            {
                Имя = FullNameTextBox.Text,
                Отдел = DepNameTextBox.Text,
                Фамилия = PositionTextBox.Text
            };

            // Поиск максимального ID среди существующих сотрудников
            int maxId = _dbContext.Сотрудники.Max(s => s.ID_сотрудника);

            // Присвоение новому сотруднику ID на 1 больше максимального
            newEmployee.ID_сотрудника = maxId + 1;

            _dbContext.Сотрудники.Add(newEmployee); // Добавление нового сотрудника в базу данных
            _dbContext.SaveChanges(); // Сохранение изменений
            this.Close(); // Закрытие окна после добавления
        }

        private void PositionTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DepNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
