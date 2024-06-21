using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecieptsApp.Classes
{
    public class Ingredient
    {
        public string Name { get; set; }

        public int Price { get; set; }

        public int DimensionPrice { get; set; }

        public int Count { get; set; }

        public string Dimension { get; set; }

        public string CompositePrice { get { return $"{Price} руб. за {DimensionPrice} {Dimension}"; } }

        public string CompositeCount { get { return $"{Count} {Dimension}"; } }

        public override string ToString() => $"{Name}";
    }
}
