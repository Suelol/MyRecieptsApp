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
    /// Логика взаимодействия для DishPage.xaml
    /// </summary>
    public partial class DishPage : Page
    {
        private Dish selectedDish;
        private DishesManager Dishes;

        public DishPage(Dish dish, DishesManager dishes)
        {
            InitializeComponent();
            selectedDish = dish;
            Dishes = dishes;
            LoadData();
        }

        private void LoadData()
        {
            Header.Content = $"Рецепт для \"{selectedDish.Name}\"";
            CategoryL.Content = $"Категория: {selectedDish.Category.Name}";
            TimeL.Content = $"Время приготовления: {selectedDish.Time} мин.";
            PriceL.Content = $"Общая стоимость: {selectedDish.PricePerOne} руб.";
            foreach(var i in selectedDish.Ingredients)
            {
                IngredientsDataGrid.Items.Add(i);
            }
            DescriptionTB.Text = selectedDish.Description;
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e) => e.Handled = (new Regex("[^0-9]+")).IsMatch(e.Text);

        private void MinusCountPButton_Click(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32(CountPTB.Text);
            if (a != 1)
            {
                a -= 1;
                CountPTB.Text = "" + a;
                PriceL.Content = $"Общая стоимость: {selectedDish.PricePerOne * a} руб.";
                foreach (var i in IngredientsDataGrid.Items)
                {
                    if (i is IngredientsCount ing)
                    {
                        if (ing.Portition == 0) { ing.Portition = 1; }
                        ing.Count = selectedDish.Ingredients[selectedDish.Ingredients.IndexOf(ing)].Count / ing.Portition * a;
                        ing.Portition = a;
                    }
                }
                IngredientsDataGrid.Items.Refresh();
            }
        }
        private void PlusCountPButton_Click(object sender, RoutedEventArgs e)
        {
            int a = Convert.ToInt32(CountPTB.Text);
            a += 1;
            CountPTB.Text = "" + a;
            PriceL.Content = $"Общая стоимость: {selectedDish.PricePerOne * a} руб.";
            foreach (var i in IngredientsDataGrid.Items)
            {
                if (i is IngredientsCount ing)
                {
                    if (ing.Portition == 0) { ing.Portition = 1; }
                    ing.Count = selectedDish.Ingredients[selectedDish.Ingredients.IndexOf(ing)].Count / ing.Portition * a;
                    ing.Portition = a;
                }
            }
            IngredientsDataGrid.Items.Refresh();
        }

        private void CountPTB_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                int a = Convert.ToInt32(CountPTB.Text);
                PriceL.Content = $"Общая стоимость: {selectedDish.PricePerOne * a} руб.";
                foreach (var i in IngredientsDataGrid.Items)
                {
                    if (i is IngredientsCount ing)
                    {
                        if (ing.Portition == 0) { ing.Portition = 1; }
                        ing.Count = selectedDish.Ingredients[selectedDish.Ingredients.IndexOf(ing)].Count / ing.Portition * a;
                        ing.Portition = a;
                    }
                }
                IngredientsDataGrid.Items.Refresh();

            }
            catch { }
        }
        private void CountPTB_LostFocus(object sender, RoutedEventArgs e)
        {
            int a = 1;
            try
            {
                a = Convert.ToInt32(CountPTB.Text);
            }
            catch { CountPTB.Text = "1"; }
            
            PriceL.Content = $"Общая стоимость: {selectedDish.PricePerOne * a} руб.";
            foreach (var i in IngredientsDataGrid.Items)
            {
                if (i is IngredientsCount ing)
                {
                    if (ing.Portition == 0) { ing.Portition = 1; }
                    ing.Count = selectedDish.Ingredients[selectedDish.Ingredients.IndexOf(ing)].Count / ing.Portition * a;
                    ing.Portition = a;
                }
            }
            IngredientsDataGrid.Items.Refresh();
        }

        private void ExitDishPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void DeleteDishButton_Click(object sender, RoutedEventArgs e)
        {
            Dishes.RemoveSelectedDish(selectedDish);
            NavigationService.Navigate(new DishesPage(Dishes));
        }

        private void EditDishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new EditDishPage(Dishes, selectedDish));
        }
    }
}
