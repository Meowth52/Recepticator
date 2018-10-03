using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recepticator
{
    class Table
    {
        public string Name { get; }
        Dictionary<string, string> Columns;
        public Table(string name, Dictionary<String, string> columns)
        {
            Name = name;
            Columns = new Dictionary<string, string>(columns);
        }
        public string getCreateCommand()
        {
            StringBuilder Building = new StringBuilder();
            Building.Append("CREATE TABLE IF NOT EXISTS " + Name + " (");
            foreach(KeyValuePair<string, string> k in Columns)
            {
                Building.Append(k.Value + ", ");
            }
            Building.Remove(Building.Length-2,2);
            Building.Append(");");
            return Building.ToString();
        }
        public string getSelectAll()
        {
            return "SELECT * FROM " + Name;
        }
    }
}
