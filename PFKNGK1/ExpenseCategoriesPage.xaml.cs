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
    /// Логика взаимодействия для ExpenseCategoriesPage.xaml
    /// </summary>
    public partial class ExpenseCategoriesPage : Page
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();
        public ExpenseCategoriesPage()
        {
            InitializeComponent();

            LoadExpenses();
        }

        private void ExpenseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = ExpenseDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddExpenseButton.IsEnabled = true; // Добавление всегда доступно
            DeleteExpenseButton.IsEnabled = isRowSelected;
            EditExpenseButton.IsEnabled = isRowSelected;
        }

        private void LoadExpenses()
        {
            var expenses = _dbContext.Виды_расходов
                .Select(c => new {
                    ID_категории = c.ID_категории,
                    Название = c.Название_категории,
                    Описание = c.Описание,
                    Предельная_сумма = c.Предельная_сумма,
                })
                .ToList();

            ExpenseDataGrid.ItemsSource = expenses;
        }

        private void AddExpense_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления расхода
            AddExpenseWindow addExpenseWindow = new AddExpenseWindow();
            addExpenseWindow.ShowDialog(); // Отображение окна как модального
            LoadExpenses(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeleteExpense_Click(object sender, RoutedEventArgs e)
        {
            if (ExpenseDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedExpense = ExpenseDataGrid.SelectedItem;

                    int expenseCategoryId = selectedExpense.ID_категории; // Получение ID выбранной категории расходов

                    var expenseToDelete = _dbContext.Виды_расходов.Find(expenseCategoryId);

                    if (expenseToDelete != null)
                    {
                        _dbContext.Виды_расходов.Remove(expenseToDelete); // Удаление категории расходов из БД
                        _dbContext.SaveChanges(); // Сохранение изменений

                        LoadExpenses(); // Перезагрузка данных после удаления
                    }
                }
                catch (DbUpdateException ex)
                {
                    if (ex.InnerException != null && ex.InnerException.InnerException != null)
                    {
                        MessageBox.Show($"Ошибка удаления категории расходов: {ex.InnerException.InnerException.Message}");
                    }
                    else
                    {
                        MessageBox.Show($"Ошибка удаления категории расходов: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления категории расходов: {ex.Message}");
                }
            }
        }

        private void EditExpense_Click(object sender, RoutedEventArgs e)
        {
            if (ExpenseDataGrid.SelectedItem != null)
            {
                dynamic selectedExpense = ExpenseDataGrid.SelectedItem;
                int expenseCategoryId = selectedExpense.ID_категории; // Получение ID выбранной категории расходов

                // Создание нового окна для редактирования категории расходов
                EditExpenseWindow editExpenseWindow = new EditExpenseWindow(expenseCategoryId);
                editExpenseWindow.ShowDialog(); // Отображение окна как модального
                LoadExpenses(); // Перезагрузка данных после закрытия окна редактирования
            }
        }
    }
}
