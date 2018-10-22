using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Recepticator
{
    class Column
    {
        string Name;
        string DataType;
        bool IsPrimaryKey;
        string TableIfForeignKey;
        public Column(string name, string dataType, bool isPrimaryKey, string tableIfForeignKey)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = isPrimaryKey;
            TableIfForeignKey = tableIfForeignKey;
        }
        public Column(string name, string dataType, string tableIfForeignKey)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = false;
            TableIfForeignKey = tableIfForeignKey;
        }
        public Column(string name, string dataType, bool isPrimaryKey)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = isPrimaryKey;
            TableIfForeignKey = "";
        }
        public Column(string name, string dataType)
        {
            Name = name;
            DataType = dataType;
            IsPrimaryKey = false;
            TableIfForeignKey = "";
        }
        public string getInitiationString()
        {
            string PrimaryKey = "";
            if (IsPrimaryKey)
                PrimaryKey = "primary key";
            return Name + " " + DataType + " " + PrimaryKey;
        }
        public bool isForeignKey()
        {
            return TableIfForeignKey != "";
        }
        public UIElement GetUIElement()
        {
            //gör switchcase och returnera rätt UIelement.
        }
    }
}
