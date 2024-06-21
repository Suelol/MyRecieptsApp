using MyRecieptsApp.Classes;
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

namespace MyRecieptsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddIngredientPage.xaml
    /// </summary>
    public partial class AddIngredientPage : Page
    {
        public AddIngredientPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DimensionIngredient.ItemsSource = new List<String> { "л.", "мл.", "г.", "кг.", "шт." };
            DimensionIngredient.SelectedIndex = 0;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameIngredient.Text == "" || PriceIngredient.Text == "" || DimensionPriceIngredient.Text == "" || CountIngredient.Text == "")
            {
                Warning.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                var res = IngredientManager.Instance.AddIngredient(new Ingredient
                {
                    Name = NameIngredient.Text,
                    Price = Convert.ToInt32(PriceIngredient.Text),
                    Dimension = DimensionIngredient.SelectedValue.ToString(),
                    DimensionPrice = Convert.ToInt32(DimensionPriceIngredient.Text),
                    Count = Convert.ToInt32(CountIngredient.Text)
                });
                if(res)
                {
                    NavigationService.Navigate(new IngridientsPage());
                }
            }
        }

        private void ExitAddIngredientButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void OnlyNumbers(object sender, TextCompositionEventArgs e) => e.Handled = (new Regex("[^0-9]+")).IsMatch(e.Text);
    }
}
