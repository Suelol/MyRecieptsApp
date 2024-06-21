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
using System.Windows.Shapes;

namespace MyRecieptsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddIngredient.xaml
    /// </summary>
    public partial class AddIngredient : Window
    {
        public Ingredient ingredient {  get; private set; }
        public int Count { get; private set; }
        public AddIngredient()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            IngredientsComboBox.ItemsSource = IngredientManager.Instance.Ingredients;
        }

        private void AddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            if(IngredientsComboBox.SelectedIndex == -1 || CountTB.Text == "" || CountTB.Text == "0")
            {
                MessageBox.Show("Заполните все поля и количество не может быть 0!");
                return;
            }
            ingredient = IngredientManager.Instance.Ingredients[IngredientsComboBox.SelectedIndex];
            Count = Convert.ToInt32(CountTB.Text);
            DialogResult = true;
        }

        private void OnlyNumbers(object sender, TextCompositionEventArgs e) => e.Handled = (new Regex("[^0-9]+")).IsMatch(e.Text);

        private void ExitAddIngredientButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
