using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MyRecieptsApp.Classes
{
    public class IngredientManager
    {
        private static IngredientManager _instance;
        private static readonly object _lock = new object();

        public static IngredientManager Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new IngredientManager();
                    }
                    return _instance;
                }
            }
        }

        public List<Ingredient> Ingredients { get; private set; }

        private IngredientManager()
        {
            // Initial ingredients can be loaded here or from a persistent storage
            Ingredients = new List<Ingredient>
            {
                new Ingredient(){ Name = "Картофель", Count = 18, Dimension = "шт.", Price = 5, DimensionPrice = 1 },
                new Ingredient(){ Name = "Помидоры", Count = 52, Dimension = "шт.", Price = 7, DimensionPrice = 1 },
                new Ingredient(){ Name = "Лук", Count = 32, Dimension = "шт.", Price = 3, DimensionPrice = 1 },
                new Ingredient(){ Name = "Огурец", Count = 16, Dimension = "шт.", Price = 6, DimensionPrice = 1 },
                new Ingredient(){ Name = "Кабачок", Count = 7, Dimension = "шт.", Price = 11, DimensionPrice = 1 },
                new Ingredient(){ Name = "Свекла", Count = 16, Dimension = "шт.", Price = 8, DimensionPrice = 1 },
                new Ingredient(){ Name = "Говядина", Count = 50, Dimension = "кг.", Price = 400, DimensionPrice = 1 },
                new Ingredient(){ Name = "Баранина", Count = 30, Dimension = "кг.", Price = 366, DimensionPrice = 1 },
                new Ingredient(){ Name = "Баклажан", Count = 40, Dimension = "шт.", Price = 14, DimensionPrice = 1 },
                new Ingredient(){ Name = "Морковь", Count = 22, Dimension = "шт.", Price = 9, DimensionPrice = 1 },
            };
        }

        public bool AddIngredient(Ingredient ingredient)
        {
            foreach (var i in Ingredients)
            {
                if (i.Name == ingredient.Name)
                {
                    MessageBox.Show("Такой ингредиент уже существует!");
                    return false;
                }
            }
            Ingredients.Add(ingredient);
            return true;
        }

        public void RemoveIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }

        public void UpdateIngredient(Ingredient oldIngredient, Ingredient newIngredient)
        {
            var index = Ingredients.IndexOf(oldIngredient);
            if (index != -1)
            {
                Ingredients[index] = newIngredient;
            }
        }
    }
}
