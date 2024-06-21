using MyRecieptsApp.Classes;
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
using System.Windows.Shapes;

namespace MyRecieptsApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddCategory.xaml
    /// </summary>
    public partial class AddCategory : Window
    {
        public AddCategory()
        {
            InitializeComponent();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameCategoryTB.Text == "")
            {
                MessageBox.Show("Заполните все поля!");
                return;
            }
            CategoriestManager.Instance.AddCategory(new Category { Name = NameCategoryTB.Text });
            this.Close();
        }

        private void ExitAddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
