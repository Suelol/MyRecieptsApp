using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyRecieptsApp.Classes
{
    public class IngredientsCount
    {
        public Ingredient ingredient { get; set; }

        public int Count { get; set; }

        public int Portition { get; set; }

        public string Price { 
            get
            {
                return $"{ingredient.Price * Count} руб.";
            }
        }

        public string Have {
            get
            {
                foreach (var ing in IngredientManager.Instance.Ingredients)
                {
                    if (ing.Name == ingredient.Name)
                    {
                        if (Count > ing.Count)
                        {
                            return "◉";//"✖";
                        }
                        else
                        {
                            return "○";//"✔";
                        }
                    }
                }
                return "◉";//"✖";
            }
        }
    }

    public class DishesManager : INotifyPropertyChanged
    {
        private Dish _selectedDish;

        public List<Dish> Dishes { get; set; }

        public Dish SelectedDish
        {
            get => _selectedDish;
            set
            {
                _selectedDish = value;
                OnPropertyChanged();
            }
        }

        public DishesManager()
        {
            LoadDishes();
        }

        private void LoadDishes()
        {
            Dishes = new List<Dish>
            {
                new Dish {
                    Name = "Яичница1",
                    Image = "/Images/dish1.jpg",
                    Description = "asdawdwa",
                    Category = CategoriestManager.Instance.Categories[1],
                    Time = 45,
                    Ingredients = new List<IngredientsCount>
                    {
                        new IngredientsCount { ingredient = IngredientManager.Instance.Ingredients[0], Count = 10 },
                    }
                }
            };
        }

        public void RemoveSelectedDish(Dish dish)
        {
            Dishes.Remove(dish);
        }

        public void AdddDish(Dish dish)
        {
            Dishes.Add(dish);
        }

        public void UpdateDish(Dish oldDish, Dish newDish)
        {
            var index = Dishes.IndexOf(oldDish);
            if (index != -1)
            {
                Dishes[index] = newDish;
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
