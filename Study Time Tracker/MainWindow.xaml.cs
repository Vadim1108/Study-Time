using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using System.Windows.Forms;

namespace Study_Time_Tracker
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public SQLiteConnection connection;
        string SQLPath = "Data Source=Data.db";

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                connection = new SQLiteConnection(SQLPath);
                connection.Open();

                TextField1.Text = SQLReadValue(1, "Fields");
                TextField1Hours.Text = SQLReadValue(1, "Hours");
                TextField1Minutes.Text = SQLReadValue(1, "Minutes");
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Something goes wrong during loading", "Error", MessageBoxButtons.OK);
            }
        }

        string SQLReadValue(int ID, string columnName)
        {   
            SQLiteCommand command = new SQLiteCommand
            { Connection = connection, CommandText = $"SELECT {columnName} FROM ApplicationData WHERE ID={ID}" };
            
            SQLiteDataReader SQLReader = command.ExecuteReader();

            while (SQLReader.Read())
                return $@"{SQLReader[$"{columnName}"]}";
            return "";
        } 

        void SQLUpdateField (int ID, string newFieldText)
        {
            SQLiteCommand command = new SQLiteCommand
            { Connection = connection, CommandText =$"UPDATE ApplicationData set Fields='{newFieldText}' WHERE ID={ID}" };
            command.ExecuteNonQuery();

        }

        private void TextField1_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                SQLUpdateField(1, TextField1.Text);
                TextField1.Text = SQLReadValue(1, "Fields");

            }
        }
    }
}

