using Microsoft.Win32;
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
    /// Логика взаимодействия для EditDishPage.xaml
    /// </summary>
    public class dopDish
    {
        public Ingredient ingredient { get; set; }

        public string Name { get { return ingredient.Name; } }

        public int Count { get; set; }


        public string Dimension { get { return ingredient.Dimension; } }

        public int Price { get { return ingredient.Price * Count; } }
    }

    public partial class EditDishPage : Page
    {

        public DishesManager Dishes;
        public Dish Dish;
        public EditDishPage(DishesManager dishes, Dish dish)
        {
            InitializeComponent();
            Dishes = dishes;
            Dish = dish;
            LoadData();
        }

        private void LoadData()
        {
            DishName.Text = Dish.Name;
            CategoriesComboBox.Items.Clear();
            foreach (var c in CategoriestManager.Instance.Categories)
            {
                CategoriesComboBox.Items.Add(c);
            }
            CategoriesComboBox.Items.Add(new Category { Name = "Добавить" });
            CategoriesComboBox.SelectedIndex = CategoriestManager.Instance.Categories.IndexOf(Dish.Category);
            TimeTB.Text = Dish.Time.ToString();
            SelectedImage.Source = new BitmapImage(new Uri("pack://application:,,," + Dish.Image));
            DescriptionTB.Text = Dish.Description;
            foreach (var i in Dish.Ingredients)
            {
                IngredientsDataGrid.Items.Add(new dopDish { ingredient = i.ingredient, Count = i.Count});
            }
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e) => e.Handled = (new Regex("[^0-9]+")).IsMatch(e.Text);

        private void ExitAddDishButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoriesComboBox.SelectedIndex == CategoriestManager.Instance.Categories.Count)
            {
                var cc = CategoriestManager.Instance.Categories.Count;
                AddCategory addCategory = new AddCategory();
                addCategory.ShowDialog();
                if (CategoriestManager.Instance.Categories.Count > cc)
                {
                    CategoriesComboBox.Items.Clear();
                    foreach (var c in CategoriestManager.Instance.Categories)
                    {
                        CategoriesComboBox.Items.Add(c);
                    }
                    CategoriesComboBox.Items.Add(new Category { Name = "Добавить" });
                    CategoriesComboBox.SelectedIndex = CategoriesComboBox.Items.Count - 2;
                }
                else
                {
                    CategoriesComboBox.SelectedIndex = 0;
                }
            }
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            AddIngredient addIngredient = new AddIngredient();
            if (addIngredient.ShowDialog() == true)
            {
                Ingredient ingredient = addIngredient.ingredient;
                int count = addIngredient.Count;
                foreach (var i in IngredientsDataGrid.Items)
                {
                    if (i is dopDish dish)
                    {
                        if (dish.ingredient == ingredient)
                        {
                            dish.Count += count;
                            IngredientsDataGrid.Items.Refresh();
                            return;
                        }
                    }
                }
                IngredientsDataGrid.Items.Add(new dopDish { ingredient = ingredient, Count = count });
            }
        }

        private void DeleteIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is dopDish selectedIngredient)
            {
                IngredientsDataGrid.Items.Remove(selectedIngredient);
            }
        }

        private void PlusCountButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is dopDish selectedIngredient)
            {
                selectedIngredient.Count += 1;
                IngredientsDataGrid.Items.Refresh();
            }
        }

        private void MinusCountButton_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsDataGrid.SelectedItem is dopDish selectedIngredient)
            {
                if (selectedIngredient.Count > 1)
                {
                    selectedIngredient.Count -= 1;
                    IngredientsDataGrid.Items.Refresh();
                }

            }
        }

        private void IngredientsDataGrid_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            return;
        }

        private void SelectPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage(new Uri(openFileDialog.FileName));
                SelectedImage.Source = bitmap;
            }
        }

        private void SaveDishButton_Click(object sender, RoutedEventArgs e)
        {
            if (DishName.Text == "" || TimeTB.Text == "" || DescriptionTB.Text == "" || IngredientsDataGrid.Items.Count == 0 || CategoriesComboBox.SelectedIndex == 0)
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            List<IngredientsCount> ingredientsCounts = new List<IngredientsCount>();
            foreach (var i in IngredientsDataGrid.Items)
            {
                if (i is dopDish dish)
                {
                    ingredientsCounts.Add(new IngredientsCount { ingredient = dish.ingredient, Count = dish.Count });
                }
            }
            Dish newDish = new Dish
            {
                Name = DishName.Text,
                Category = CategoriestManager.Instance.Categories[CategoriesComboBox.SelectedIndex],
                Description = DescriptionTB.Text,
                Image = SelectedImage.Source.ToString(),
                Time = Convert.ToInt32(TimeTB.Text),
                Ingredients = ingredientsCounts
            };
            Dishes.Dishes[Dishes.Dishes.IndexOf(Dish)] = newDish;
            NavigationService.Navigate(new DishPage(newDish, Dishes));
        }
    }
}
