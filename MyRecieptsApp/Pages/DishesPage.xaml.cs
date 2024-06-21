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
using MyRecieptsApp.Classes;

namespace MyRecieptsApp.Pages
{
    public class dopDishes
    {
        public List<Dish> Dishes { get; set; }
    }
    /// <summary>
    /// Логика взаимодействия для DishesPage.xaml
    /// </summary>
    public partial class DishesPage : Page
    {
        private DishesManager Dishess;
        public DishesPage(DishesManager dishes)
        {
            InitializeComponent();
            Dishess = dishes;
            LoadData();
        }

        private void LoadData()
        {
            DataContext = Dishess;
            CategoriesComboBox.ItemsSource = CategoriestManager.Instance.Categories;
            CategoriesComboBox.SelectedIndex = 0;
        }

        private void AddDishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddDishPage(Dishess));
        }

        private void OnDishClicked(object sender, MouseButtonEventArgs e)
        {
            if (sender is Border border && border.DataContext is Dish selectedDish)
            {
                NavigationService.Navigate(new DishPage(selectedDish, Dishess));
            }
        }

        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesComboBox.SelectedIndex == 0)
            {
                DataContext = Dishess;
                return;
            }
            List<Dish> dishes = new List<Dish>();
            foreach (var d in Dishess.Dishes)
            {
                if (d.Category == CategoriesComboBox.SelectedValue)
                    dishes.Add(d);
            }
            DataContext = new dopDishes { Dishes = dishes };
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (NameSearch.Text == "")
            {
                DataContext = Dishess;
                return;
            }
            List<Dish> dishes = new List<Dish>();
            foreach (var d in Dishess.Dishes)
            {
                if (d.Name.ToLower().Contains(NameSearch.Text.ToLower()) )
                    dishes.Add(d);
            }
            DataContext = new dopDishes { Dishes = dishes };
        }

        private void OnlyIng_Click(object sender, RoutedEventArgs e)
        {
            if (OnlyIng.IsChecked == true)
            {
                List<Dish> dishes = new List<Dish>();
                foreach (var d in Dishess.Dishes)
                {
                    var t = true;
                    foreach (var i in d.Ingredients)
                    {
                        if (i.Have == "◉")
                        {
                            t = false;
                            break;
                        }
                    }
                    if (t) dishes.Add(d);
                }
                DataContext = new dopDishes { Dishes = dishes };
            }
            else
            {
                DataContext = Dishess;
            }
        }
    }
}
