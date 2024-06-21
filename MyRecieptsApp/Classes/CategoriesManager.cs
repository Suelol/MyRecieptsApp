using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyRecieptsApp.Classes
{
    public class CategoriestManager
    {
        private static CategoriestManager _instance;
        private static readonly object _lock = new object();

        public static CategoriestManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new CategoriestManager();
                    }
                    return _instance;
                }
            }
        }

        public List<Category> Categories { get; private set; }

        private CategoriestManager()
        {
            // Initial ingredients can be loaded here or from a persistent storage
            Categories = new List<Category>
            {
                new Category() { Name = "Не выбрана" },
                new Category(){ Name = "Мясные блюда" },
                new Category(){ Name = "Горячие блюда" },
                new Category(){ Name = "Напитки" },
                new Category(){ Name = "Холодные блюда" },
                new Category(){ Name = "Бар" },
                new Category(){ Name = "Супы" },
            };
        }

        public bool AddCategory(Category category)
        {
            foreach (var i in Categories)
            {
                if (i.Name == category.Name)
                {
                    MessageBox.Show("Такой ингредиент уже существует!");
                    return false;
                }
            }
            Categories.Add(category);
            return true;
        }

        public void RemoveCategory(Category category)
        {
            Categories.Remove(category);
        }

        public void UpdateCategory(Category oldCategory, Category newCategory)
        {
            var index = Categories.IndexOf(oldCategory);
            if (index != -1)
            {
                Categories[index] = newCategory;
            }
        }
    }
}
