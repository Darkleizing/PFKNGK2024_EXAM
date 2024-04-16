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
    /// Логика взаимодействия для AddPurchaseWindow.xaml
    /// </summary>
    public partial class AddPurchaseWindow : Window
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();

        public AddPurchaseWindow()
        {
            InitializeComponent();
            LoadEmployees();
            LoadExpenseCategories();
            Loaded += AddExpenseWindow_Loaded;
        }

        private void AddExpenseWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadEmployees();
            LoadExpenseCategories();
        }

        private void LoadEmployees()
        {
            var employees = _dbContext.Сотрудники.ToList();
            EmployeeComboBox.ItemsSource = employees;
        }

        private void LoadExpenseCategories()
        {
            var expenseCategories = _dbContext.Виды_расходов.ToList();
            ExpenseCategoryComboBox.ItemsSource = expenseCategories;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (EmployeeComboBox.SelectedItem != null && ExpenseCategoryComboBox.SelectedItem != null)
            {
                var selectedEmployee = (Сотрудники)EmployeeComboBox.SelectedItem;
                var selectedExpenseCategory = (Виды_расходов)ExpenseCategoryComboBox.SelectedItem;

                int maxPurchaseId = _dbContext.Покупки.Max(p => p.ID_покупки); // Получаем максимальный ID покупки
                var newPurchase = new Покупки
                {
                    ID_покупки = maxPurchaseId + 1, // Присваиваем новому ID значение на 1 больше максимального
                    ID_сотрудника = selectedEmployee.ID_сотрудника,
                    ID_категории_расходов = selectedExpenseCategory.ID_категории,
                    Дата = DatePicker.SelectedDate,
                    Сумма = Convert.ToDecimal(AmountTextBox.Text)
                };
                _dbContext.Покупки.Add(newPurchase);
                _dbContext.SaveChanges();
                this.Close();
            }
            else
            {
                // Обработка случая, когда значения не выбраны
            }
        }
    }
}
