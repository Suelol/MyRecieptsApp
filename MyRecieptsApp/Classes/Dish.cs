using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecieptsApp.Classes
{
    public class Dish
    {
        public string Name { get; set; }

        public string Image {  get; set; }

        public string Description { get; set; }

        public Category Category { get; set; }

        public int Time {  get; set; }

        public List<IngredientsCount> Ingredients { get; set; }

        public int PricePerOne { get {
                var sum = 0;
                foreach (var item in Ingredients) { sum += item.Count * item.ingredient.Price; }
                return sum;
            }
        }
    }
}
