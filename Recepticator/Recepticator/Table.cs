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
        List<Column> Columns;
        public Table(string name, List<Column> columns)
        {
            Name = name;
            Columns = new List<Column>(columns);
        }
        public string getCreateCommand()
        {
            StringBuilder Building = new StringBuilder();
            Building.Append("CREATE TABLE IF NOT EXISTS " + Name + " (");
            foreach(Column c in Columns)
            {
                Building.Append(c.getInitiationString() + ", ");
            }
            Building.Remove(Building.Length-2,2);
            Building.Append(");");
            return Building.ToString();
        }
        public string getSelectAll()
        {
            return "SELECT * FROM " + Name;
        }
        public List<Column> getColumns()
        {
            return new List<Column>(Columns);
        }
    }
}
