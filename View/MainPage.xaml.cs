using BankTest.Model;
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

namespace BankTest.View
{
    public partial class MainPage : Page
    {
        private string userRole;


        public MainPage(string userRole)
        {
            InitializeComponent();
            this.userRole = userRole;
            var alltypes = AppData.db.Category.ToList();
            alltypes.Insert(0, new Category
            {
                Title = "Все категории"
            });
            CmbCateory.ItemsSource = alltypes;
            CmbCateory.SelectedIndex = 0;
            UpdateProducts();

        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (userRole == "Администратор")
            {
                UserGrid.Visibility = Visibility.Hidden;
                Search.Visibility = Visibility.Hidden;
                CmbCateory.Visibility = Visibility.Hidden;

                AdminGrid.Visibility = Visibility.Visible;
                Admin_Panel.Visibility = Visibility.Visible;
              
                AdminGrid.ItemsSource = AppData.db.Category.ToList();
                AdminGrid.Columns[0].IsReadOnly = true;

            }

            else if (userRole == "Продвинутый пользователь")
            {
                
                UserGrid.ItemsSource = AppData.db.Product.ToList();
            }

            else
            {
                Admin_Panel.Visibility = Visibility.Hidden;
                UserGrid.Columns[6].Visibility = Visibility.Hidden;
                UserGrid.IsReadOnly = true;
                UserGrid.ItemsSource = AppData.db.Product.ToList();
            }

        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userRole == "Администратор") NavigationService.Navigate(new NewCategory());
            else NavigationService.Navigate(new NewProduct());
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            AppData.db.SaveChanges();
        }

        private void RemoveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (userRole == "Продвинутый пользователь")
            {
                if (MessageBox.Show("Вы хотите удалить товар", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var currentProduct = UserGrid.SelectedItem as Product;
                    AppData.db.Product.Remove(currentProduct);
                    AppData.db.SaveChanges();
                    UserGrid.ItemsSource = AppData.db.Product.ToList();
                }
            }
            else
            {
                if (MessageBox.Show("Вы хотите удалить категорию", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    var currentCategory = AdminGrid.SelectedItem as Category;
                    AppData.db.Category.Remove(currentCategory);
                    AppData.db.SaveChanges();
                    AdminGrid.ItemsSource = AppData.db.Category.ToList();
                }
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            UpdateProducts(); 
        }

        private void UpdateProducts()
        { 
            var currentProduct = AppData.db.Product.ToList().Where(x => x.Name.ToLower().Contains(Search.Text.ToLower()));
            if (CmbCateory.SelectedIndex > 0)
            {
                currentProduct = currentProduct.Where(p => p.CategoryID == CmbCateory.SelectedIndex);
            }
            UserGrid.ItemsSource = currentProduct.ToList();
        }

        private void CmbCateory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateProducts();
        }

        

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignIn());
        }
    }
}
