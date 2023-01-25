using BankTest.Model;
using BankTest.View;
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


namespace BankTest
{
    /// <summary>
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public SignIn()
        {
            InitializeComponent();
        }

        private void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = AppData.db.Users.FirstOrDefault(u => u.Username == TxbLogin.Text && u.Password == TxbPassword.Password);

            if (currentUser != null)
            {
                var currentRole = AppData.db.Role.FirstOrDefault(r => r.ID == currentUser.RoleID);
                NavigationService.Navigate(new MainPage(currentRole.Title));
            }
            else
            {
                MessageBox.Show("Проверьте логин и пароль");
            }
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new SignUp());
        }
    }
}
