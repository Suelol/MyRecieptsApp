using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyRecieptsApp.Classes
{
    public class Category
    {
        public string Name { get; set; }

        public override string ToString() => $"{Name}";
    }
}
