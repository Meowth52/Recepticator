using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recepticator
{
    class Ingredient
    {
        public string Name { get; }
        public string Unit { get; }
        public Ingredient(string name, string unit)
        {
            Name = name;
            Unit = unit;
        }
    }
}
