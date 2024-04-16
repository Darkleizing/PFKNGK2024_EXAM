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
    /// Логика взаимодействия для EditExpenseWindow.xaml
    /// </summary>
    public partial class EditExpenseWindow : Window
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();
        private int _expenseCategoryId;

        public EditExpenseWindow(int expenseCategoryId)
        {
            InitializeComponent();
            _expenseCategoryId = expenseCategoryId;
            LoadExpenseData();
        }

        private void LoadExpenseData()
        {
            // Загрузка информации о категории расходов по ее ID
            var expenseCategory = _dbContext.Виды_расходов
                .Where(c => c.ID_категории == _expenseCategoryId)
                .Select(c => new
                {
                    ID_Категории = c.ID_категории,
                    Название = c.Название_категории,
                    Описание = c.Описание,
                    Предельная_сумма = c.Предельная_сумма,
                })
                .FirstOrDefault();

            if (expenseCategory != null)
            {
                // Заполнение полей формы информацией о категории расходов
                NameTextBox.Text = expenseCategory.Название;
                DescriptionTextBox.Text = expenseCategory.Описание;
                LimitTextBox.Text = expenseCategory.Предельная_сумма.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Сохранение изменений информации о категории расходов
            var expenseCategory = _dbContext.Виды_расходов.Find(_expenseCategoryId);
            if (expenseCategory != null)
            {
                expenseCategory.Название_категории = NameTextBox.Text;
                expenseCategory.Описание = DescriptionTextBox.Text;
                expenseCategory.Предельная_сумма = Convert.ToDecimal(LimitTextBox.Text);
                _dbContext.SaveChanges(); // Сохранение изменений в базе данных
            }
            this.Close(); // Закрытие окна после сохранения
        }
    }
}
