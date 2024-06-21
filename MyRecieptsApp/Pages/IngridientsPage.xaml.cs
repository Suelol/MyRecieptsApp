using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MyRecieptsApp.Classes;

namespace MyRecieptsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для IngridientsPage.xaml
    /// </summary>
    public partial class IngridientsPage : Page
    {
        private int stran = 1;
        public IngridientsPage()
        {
            InitializeComponent();
            LoadIngredients();
        }

        private void LoadIngredients()
        {
            if (IngredientManager.Instance.Ingredients.Count == 0) { stran = 0; }
            stranica.Content = $"{stran}/{Math.Ceiling((double)IngredientManager.Instance.Ingredients.Count / 5)}";
            IngredientsDataGrid.ItemsSource = IngredientManager.Instance.Ingredients.Skip(5*(stran-1)).Take(5);
            countIngredients.Content = $"{IngredientManager.Instance.Ingredients.Count} наименований";
            double sum = 0;
            foreach(var i in IngredientManager.Instance.Ingredients)
            {
                sum += i.Price * ((double)i.Count / i.DimensionPrice);
            }
            moneyIngredients.Content = $"Запасов в холодильнике на сумму (руб.): {(int)sum}";
        }

        private void FirstStranicaButton_Click(object sender, RoutedEventArgs e)
        {
            if (stran != 1)
            {
                stran = 1;
                LoadIngredients();
            }
        }

        private void PreviousStranicaButton_Click(object sender, RoutedEventArgs e)
        {
            if(stran != 1)
            {
                stran -= 1;
                LoadIngredients();
            }
        }

        private void NextStranicaButton_Click(object sender, RoutedEventArgs e)
        {
            if(stran != Math.Ceiling((double)IngredientManager.Instance.Ingredients.Count / 5))
            {
                stran += 1;
                LoadIngredients();
            }
        }

        private void LastStranicaButton_Click(object sender, RoutedEventArgs e)
        {
            if (stran != Math.Ceiling((double)IngredientManager.Instance.Ingredients.Count / 5))
            {
                stran = (int)Math.Ceiling((double)IngredientManager.Instance.Ingredients.Count / 5);
                LoadIngredients();
            }
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddIngredientPage());
        }

        private void DeleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is Ingredient selectedIngredient)
            {
                IngredientManager.Instance.RemoveIngredient(selectedIngredient);
                LoadIngredients();
            }
        }

        private void EditIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is Ingredient selectedIngredient)
                NavigationService.Navigate(new EditIngredientPage(selectedIngredient));
        }

        private void PlusCountButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is Ingredient selectedIngredient)
            {
                selectedIngredient.Count++;
                LoadIngredients();
            }
        }

        private void MinusCountButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is Ingredient selectedIngredient)
            {
                if (selectedIngredient.Count != 0)
                {
                    selectedIngredient.Count--;
                    LoadIngredients();
                }
            }
        }
    }
}
