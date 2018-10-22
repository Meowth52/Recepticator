using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Recepticator
{
    public partial class MainWindow : Window
    {
        private readonly MainView _mainView;
        List<Ingredient> TestData;
        Dictionary<string, Table> Tables;
        public MainWindow()
        {
            InitializeComponent();
            _mainView = DataContext as MainView;
            if (!System.IO.File.Exists("Test1.sqlite"))
            {
                SQLiteConnection.CreateFile("Test1.sqlite");
            }
            // create a new database connection:
            SQLiteConnection SqlConnectionn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");

            // open the connection:
            SqlConnectionn.Open();
            SQLiteCommand Command = SqlConnectionn.CreateCommand();

            // Set up tables
            Tables = new Dictionary<string, Table>
            {
                { "Ingredients",new Table("Ingredients", new List<Column>{ new Column("Ingredient", "varchar(100)", true), new Column("Unit", "varchar(100)", "Units") }) },
                { "Units",new Table("Units", new List<Column>{ new Column("Unit", "varchar(100)", true) }) }
            };
            foreach (KeyValuePair<string, Table> t in Tables)
            {
                Command.CommandText = t.Value.getCreateCommand();
                Command.ExecuteNonQuery();
            }

            Command.CommandText = "SELECT * FROM Ingredients";
            SQLiteDataReader sqlite_datareader = Command.ExecuteReader();
            List<Ingredient> FoundIngredients = new List<Ingredient>();
            // The SQLiteDataReader allows us to run through each row per loop
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                // Print out the content of the text field:
                // System.Console.WriteLine("DEBUG Output: '" + sqlite_datareader["text"] + "'");

                object idReader = sqlite_datareader.GetValue(0);
                string textReader = sqlite_datareader.GetString(1);
                FoundIngredients.Add(new Ingredient(sqlite_datareader.GetString(0), sqlite_datareader.GetString(1)));

                OutTextBox.Text += idReader + " '" + textReader + "' " + "\n";
            }
            _mainView.OutputIngredient = FoundIngredients;
        }

        private void NewMockQuery(object sender, RoutedEventArgs e)
        {
            // Ha Table att returnera en lista med column och column att returnera "new TextBox()" eller motsvarande

            AttributeInput.Children.Add(new TextBox());
            AttributeInput.Children.Add(new TextBox());
            OutTextBox.Text = "";
            
        }

        private void SubmittPress(object sender, RoutedEventArgs e)
        {
            SQLiteConnection SqlConnection = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");
            SqlConnection.Open();
            SQLiteCommand Query = SqlConnection.CreateCommand();
            List<Ingredient> FoundIngredients = new List<Ingredient>();
            TextBox Attribute0 = (TextBox)AttributeInput.Children[0];
            TextBox Attribute1 = (TextBox)AttributeInput.Children[1];
            Ingredient NewIngredient = new Ingredient(Attribute0.Text, Attribute1.Text);
            Query.CommandText = NewIngredient.getInsert();
            Query.ExecuteNonQuery();
            Query.CommandText = Tables["Ingredients"].getSelectAll();
            SQLiteDataReader Reader = Query.ExecuteReader();
            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {
                FoundIngredients.Add(new Ingredient(Reader));
            }
            _mainView.OutputIngredient = FoundIngredients;
        }
    }
}