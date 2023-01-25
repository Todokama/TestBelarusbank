using BankTest.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class NewProduct : Page
    {
        public NewProduct()
        {
            InitializeComponent();
            CmbCategory.ItemsSource = AppData.db.Category.ToList();
            CmbCategory.SelectedIndex = 0;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (AppData.db.Product.FirstOrDefault(p => p.Name == TxbName.Text) == null)
            {
                if(!IsValid())
                {
                    MessageBox.Show("Проверьте введеные данные", "Уведомление");
                }
                else
                {
                    Product product = new Product();

                    var currentCategory = CmbCategory.SelectedItem as Category;

                    product.Name = TxbName.Text;
                    product.Price = Convert.ToDecimal(TxbPrice.Text);
                    product.Product_description = TxbDescription.Text;
                    product.Genral_note = TxbGeneralNote.Text;
                    product.Special_note = TxbSpecialNote.Text;
                    product.CategoryID = currentCategory.ID;

                    AppData.db.Product.Add(product);
                    AppData.db.SaveChanges();
                    NavigationService.GoBack();
                }
                
            }
            else MessageBox.Show("Товар с таким именем уже существует", "Уведомление");
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        
        private bool IsValid()
        {
            if (String.IsNullOrEmpty(TxbName.Text) == true) return false;
            else if (String.IsNullOrEmpty(TxbPrice.Text) == true) return false;
            else if (String.IsNullOrEmpty(TxbDescription.Text) == true) return false;
            else if (!IsTextAllowed(TxbPrice.Text)) return false;
            else if (String.IsNullOrEmpty(TxbGeneralNote.Text) == true) return false;
            else if (String.IsNullOrEmpty(TxbSpecialNote.Text) == true) return false;
            else return true;
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

    }
}
