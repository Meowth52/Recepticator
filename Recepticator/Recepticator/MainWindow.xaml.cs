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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SQLiteConnection.CreateFile("Test1.sqlite");
            // create a new database connection:
            SQLiteConnection sqlite_conn = new SQLiteConnection("Data Source=Test1.sqlite;Version=3;");

            // open the connection:
            sqlite_conn.Open();
            SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand();

            // Let the SQLiteCommand object know our SQL-Query:
            sqlite_cmd.CommandText = "CREATE TABLE IF NOT EXISTs test (id int primary key, text varchar(100));";
            sqlite_cmd.ExecuteNonQuery();
            Dictionary<int, string> TestData = new Dictionary<int, string>
            {
                { 1, "ett" }
                ,{2, "två" }
                , {3, "tre" }
            };
            foreach(KeyValuePair<int, string> k  in TestData)
            {
                sqlite_cmd.CommandText = "INSERT INTO test (id, text) VALUES(" + k.Key.ToString() + ", '" + k.Value +"'); ";
                sqlite_cmd.ExecuteNonQuery();
            }
            // Now lets execute the SQL ;-)

            sqlite_cmd.CommandText = "SELECT * FROM test";
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