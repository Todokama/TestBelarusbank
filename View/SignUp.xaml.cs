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
    public partial class SignUp : Page
    {
        public SignUp()
        {
            InitializeComponent();
            CmbRole.ItemsSource = AppData.db.Role.ToList();
            CmbRole.SelectedIndex = 2;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.db.Users.FirstOrDefault(u => u.Username == TxbUsername.Text) == null)
            {
                Users user = new Users();

                var currentRole = CmbRole.SelectedItem as Role;
                if (String.IsNullOrEmpty(TxbUsername.Text) == true || String.IsNullOrEmpty(TxbPassword.Text) == true) MessageBox.Show("Поля должны быть заполнены");
                else
                {
                    user.Username = TxbUsername.Text;
                    user.Password = TxbPassword.Text;
                    user.RoleID = currentRole.ID;

                    AppData.db.Users.Add(user);
                    AppData.db.SaveChanges();
                    NavigationService.Navigate(new MainPage(currentRole.Title));
                }
            }
            else MessageBox.Show("Пользователь  с таким именем уже существует");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
