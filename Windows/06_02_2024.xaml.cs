using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace ADO.NET.Windows
{
    /// <summary>
    /// Interaction logic for _06_02_2024.xaml
    /// </summary>
    public partial class _06_02_2024 : Window
    {
        private readonly String _msConnectionString;
        private readonly String _myConnectionString;

        SqlConnection? _msСonnection;
        MySqlConnection? _myСonnection;

        public _06_02_2024()
        {
            InitializeComponent();

            var config = JsonSerializer.Deserialize<JsonElement>(
                System.IO.File.ReadAllText("appsettings.json"));

            var connectionString = config.GetProperty("ConnectionStrings");

            _msConnectionString = connectionString
                .GetProperty("LocalMS")
                .GetString()!;

            _myConnectionString = connectionString
                .GetProperty("LocalMy")
                .GetString()!;
        }

        private void ConnectMsButton_Click(object sender, RoutedEventArgs e)
        {
            _msСonnection = new SqlConnection(_msConnectionString);
            
            try
            {
                _msСonnection.Open();
                MsConnectionStatusLabel.Content = "Connected";
            }
            catch (Exception ex)
            {
                MsConnectionStatusLabel.Content = ex.Message;
            }
        }

        private void ConnectMyButton_Click(object sender, RoutedEventArgs e)
        {
            _myСonnection = new MySqlConnection(_myConnectionString);

            try
            {
                _myСonnection.Open();
                MyConnectionStatusLabel.Content = "Connected";
            }
            catch (Exception ex)
            {
                MyConnectionStatusLabel.Content = ex.Message;
            }
        }

        private void DisconnectMsButton_Click(object sender, RoutedEventArgs e)
        {
            if (_msСonnection == null) return;

            try
            {
                _msСonnection?.Close();
                MsConnectionStatusLabel.Content = "Disconnected";
            }
            catch (Exception ex)
            {
                MsConnectionStatusLabel.Content = ex.Message;
            }
        }

        private void DisonnectMyButton_Click(object sender, RoutedEventArgs e)
        {
            if (_myСonnection == null) return;

            try
            {
                _myСonnection?.Close();
                MyConnectionStatusLabel.Content = "Disconnected";
            }
            catch (Exception ex)
            {
                MyConnectionStatusLabel.Content = ex.Message;
            }
        }
    }
}
