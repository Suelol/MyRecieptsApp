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
    /// Логика взаимодействия для EditIngredientPage.xaml
    /// </summary>
    public partial class EditIngredientPage : Page
    {
        private Ingredient _ingredient;
        public EditIngredientPage(Ingredient ingredient)
        {
            InitializeComponent();
            _ingredient = ingredient;
            LoadData();
        }
        private void LoadData()
        {
            NameIngredient.Text = _ingredient.Name;
            PriceIngredient.Text = _ingredient.Price.ToString();
            DimensionPriceIngredient.Text = _ingredient.DimensionPrice.ToString();
            DimensionIngredient.ItemsSource = new List<String> { "л.", "мл.", "г.", "кг.", "шт." };
            DimensionIngredient.SelectedValue = _ingredient.Dimension;
            CountIngredient.Text = _ingredient.Count.ToString();
        }

        private void SaveIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameIngredient.Text == "" || PriceIngredient.Text == "" || DimensionPriceIngredient.Text == "" || CountIngredient.Text == "")
            {
                Warning.Visibility = Visibility.Visible;
                return;
            }
            else
            {
                IngredientManager.Instance.UpdateIngredient(_ingredient, new Ingredient
                {
                    Name = NameIngredient.Text,
                    Price = Convert.ToInt32(PriceIngredient.Text),
                    Dimension = DimensionIngredient.SelectedValue.ToString(),
                    DimensionPrice = Convert.ToInt32(DimensionPriceIngredient.Text),
                    Count = Convert.ToInt32(CountIngredient.Text)
                });
                NavigationService.Navigate(new IngridientsPage());
            }
        }

        private void ExitAddIngredientButton_Click(object sender, RoutedEventArgs e) => NavigationService.GoBack();

        private void OnlyNumbers(object sender, TextCompositionEventArgs e) => e.Handled = (new Regex("[^0-9]+")).IsMatch(e.Text);
    }
}
