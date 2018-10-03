using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

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
        public Ingredient(SQLiteDataReader reader)
        {
            Name = reader.GetString(0);
            Unit = reader.GetString(1);
        }
        public string getInsert()
        {
            return "INSERT INTO Ingredients (Ingredient, Unit) VALUES(" + Name + ", '" + Unit + "');";
        }
    }
}
