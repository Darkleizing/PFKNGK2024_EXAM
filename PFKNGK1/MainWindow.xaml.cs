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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            ConnectOdb.conObj = new PFKNGK1Entities();
        }

        private void EmployeesButton_Click(object sender, RoutedEventArgs e)
        {
            // Отображение содержимого EmployeesWindow в Frame
            MainFrame.Content = new EmployeesPage();
        }

        private void ExpenseCategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            // Отображение содержимого ExpenseCategoriesWindow в Frame
            MainFrame.Content = new ExpenseCategoriesPage();
        }

        private void PurchasesButton_Click(object sender, RoutedEventArgs e)
        {
            // Отображение содержимого PurchasesWindow в Frame
            MainFrame.Content = new PurchasesPage();
        }
    }
}
