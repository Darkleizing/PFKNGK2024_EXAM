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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PFKNGK1
{
    /// <summary>
    /// Логика взаимодействия для PurchasesPage.xaml
    /// </summary>
    public partial class PurchasesPage : Page
    {
        private readonly PFKNGK1Entities _dbContext = new PFKNGK1Entities();

        public PurchasesPage()
        {
            InitializeComponent();
            LoadPurchases();
        }

        private void PurchaseDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool isRowSelected = PurchaseDataGrid.SelectedItem != null;

            // Включаем или отключаем кнопки в зависимости от выбранной строки
            AddPurchaseButton.IsEnabled = true; // Добавление всегда доступно
            DeletePurchaseButton.IsEnabled = isRowSelected;
        }

        private void LoadPurchases()
        {
            var purchases = _dbContext.Покупки
                .Select(c => new
                {
                    ID_покупки = c.ID_покупки,
                    Имя_сотрудника = c.Сотрудники.Имя,
                    Категория_расходов = c.Виды_расходов.Название_категории,
                    Дата = c.Дата,
                    Сумма = c.Сумма,
                })
                .ToList();
            PurchaseDataGrid.ItemsSource = purchases;
        }

        private void AddPurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Создание нового окна для добавления покупки
            AddPurchaseWindow addPurchaseWindow = new AddPurchaseWindow();
            addPurchaseWindow.ShowDialog(); // Отображение окна как модального
            LoadPurchases(); // Перезагрузка данных после закрытия окна добавления
        }

        private void DeletePurchaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (PurchaseDataGrid.SelectedItem != null)
            {
                try
                {
                    dynamic selectedPurchase = PurchaseDataGrid.SelectedItem;
                    int purchaseId = selectedPurchase.ID_покупки; // Получение ID выбранной покупки

                    var purchaseToDelete = _dbContext.Покупки.Find(purchaseId);

                    if (purchaseToDelete != null)
                    {
                        _dbContext.Покупки.Remove(purchaseToDelete); // Удаление покупки из БД
                        _dbContext.SaveChanges(); // Сохранение изменений
                        LoadPurchases(); // Перезагрузка данных после удаления
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка удаления покупки: {ex.Message}");
                }
            }
        }
    }
}
