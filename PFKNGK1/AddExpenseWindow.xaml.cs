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
    /// Логика взаимодействия для AddExpenseWindow.xaml
    /// </summary>
    public partial class AddExpenseWindow : Window
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();

        public AddExpenseWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var newExpenseCategory = new Виды_расходов
            {
                ID_категории = GenerateNewExpenseCategoryId(), // Генерация нового уникального ID
                Название_категории = NameTextBox.Text,
                Описание = DescriptionTextBox.Text,
                Предельная_сумма = Convert.ToDecimal(LimitTextBox.Text)
            };

            _dbContext.Виды_расходов.Add(newExpenseCategory);
            _dbContext.SaveChanges();
            this.Close();
        }

        private int GenerateNewExpenseCategoryId()
        {
            // Логика для генерации нового уникального ID
            // Например, можно получить максимальный существующий ID и добавить к нему 1
            var maxId = _dbContext.Виды_расходов.Max(ec => ec.ID_категории);
            return maxId + 1;
        }
    }
}
