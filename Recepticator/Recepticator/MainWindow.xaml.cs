﻿using System;
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
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");

            // open the connection:
            sqlite_conn.Open();
            SQLiteCommand Command = sqlite_conn.CreateCommand();

            // Set up tables
            Tables = new Dictionary<string, Table>
            {
                { "Ingredients",new Table("Ingredients", new Dictionary<string, string>{ { "Ingredient", "Ingredient varchar(100) primary key" }, { "Unit", "Unit varchar(100)" } }) }
            };
            foreach (KeyValuePair<string, Table> t in Tables)
            {
                Command.CommandText = t.Value.getCreateCommand();
                Command.ExecuteNonQuery();
            }
            //TestData = new List<Ingredient>
            //{
            //    {new Ingredient("mjöl", "dl") },
            //    {new Ingredient("smör", "g") },
            //    {new Ingredient("ägg", "st") }
            //};
            //foreach(Ingredient k  in TestData)
            //{
            //    Command.CommandText = "SELECT * FROM Ingredients WHERE Ingredient = '" + k.Name + "';";
            //    SQLiteDataReader Reader = Command.ExecuteReader();
            //    if (!Reader.HasRows)
            //    {
            //        Reader.Close();
            //        Command.CommandText = "INSERT INTO Ingredients (Ingredient, Unit) VALUES('" + k.Name + "', '" + k.Unit + "' ); "; // fix :.|
            //        Command.ExecuteNonQuery();
            //    }
            //    else
            //        Reader.Close();
            //}
            // Now lets execute the SQL ;-)

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
            List<Ingredient> FoundIngredients = new List<Ingredient>();
            int i = TestData.Count+1;
            Ingredient NewIngredient = new Ingredient(i.ToString(), i.ToString());
            TestData.Add(NewIngredient);
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");
            sqlite_conn.Open();
            SQLiteCommand Query = sqlite_conn.CreateCommand();
            Query.CommandText = NewIngredient.getInsert();
            Query.ExecuteNonQuery();
            OutTextBox.Text = "";
            Query.CommandText = Tables["Ingredients"].getSelectAll();
            SQLiteDataReader Reader = Query.ExecuteReader();
            
            // The SQLiteDataReader allows us to run through each row per loop
            while (Reader.Read()) // Read() returns true if there is still a result line to read
            {
                FoundIngredients.Add(new Ingredient(Reader));
            }
            _mainView.OutputIngredient = FoundIngredients;
        }
        
    }
}