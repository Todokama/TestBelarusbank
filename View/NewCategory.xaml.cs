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

    public partial class NewCategory : Page
    {
        public NewCategory()
        {
            InitializeComponent();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.db.Category.FirstOrDefault(c => c.Title == TxbNewCategory.Text) == null)
            {
                Category category = new Category();
                category.Title = TxbNewCategory.Text;
                AppData.db.Category.Add(category);
                AppData.db.SaveChanges();
                NavigationService.GoBack();
            }
            else if (String.IsNullOrWhiteSpace(TxbNewCategory.Text)) MessageBox.Show("Вы не ввели название категории", "Уведомление");
            else MessageBox.Show("Такая категория уже существует", "Уведомление");
        }


        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
