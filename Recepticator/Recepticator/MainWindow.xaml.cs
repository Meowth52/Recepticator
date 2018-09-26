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
        public MainWindow()
        {
            InitializeComponent();
            _mainView = DataContext as MainView;
            SQLiteConnection.CreateFile("Test1.sqlite");
            // create a new database connection:
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");

            // open the connection:
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            // Let the SQLiteCommand object know our SQL-Query:
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTs testIngredient (IngredientType varchar(100) primary key, Unit varchar(100));";
            sqlite_cmd.ExecuteNonQuery();
            TestData = new List<Ingredient>
            {
                {new Ingredient("mjöl", "dl") },
                {new Ingredient("smör", "g") },
                {new Ingredient("ägg", "st") }
            };
            foreach(Ingredient k  in TestData)
            {
                sqlite_cmd.CommandText = "INSERT INTO testIngredient (IngredientType, Unit) VALUES('" + k.Name + "', '" + k.Unit +"'); ";
                sqlite_cmd.ExecuteNonQuery();
            }
            // Now lets execute the SQL ;-)

            sqlite_cmd.CommandText = "SELECT * FROM testIngredient";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
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
            int i = TestData.Count+1;
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO testIngredient (IngredientType, Unit) VALUES(" + i.ToString() + ", '" + i.ToString() + "'); ";
            sqlite_cmd.ExecuteNonQuery();
            OutTextBox.Text = "";
            sqlite_cmd.CommandText = "SELECT * FROM testIngredient";
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();
            // The SQLiteDataReader allows us to run through each row per loop
            while (sqlite_datareader.Read()) // Read() returns true if there is still a result line to read
            {
                // Print out the content of the text field:
                // System.Console.WriteLine("DEBUG Output: '" + sqlite_datareader["text"] + "'");

                object idReader = sqlite_datareader.GetValue(0);
                string textReader = sqlite_datareader.GetString(1);

                OutTextBox.Text += idReader + " '" + textReader + "' " + "\n";
            }
        }
    }
}