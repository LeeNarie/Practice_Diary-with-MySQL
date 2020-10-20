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
using System.Data.SqlClient;
using System.Data;

namespace DI_PrDiary
{
    /// <summary>
    /// Логика взаимодействия для AddRow.xaml
    /// </summary>
    public partial class AddRow : Window
    {
        public AddRow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string _id = adid.Text.ToString();
            string _name = adna.Text.ToString();
            int _sname = Convert.ToInt32( adsu.Text);
            string ssql = $"INSERT INTO diary_table (Дата, Тема, Оценка) VALUES ('{_id}', '{_name}', {_sname})";
            string connectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();// Открытие Соединения

            SqlCommand command = new SqlCommand(ssql, conn);
            int number = command.ExecuteNonQuery();
            Console.WriteLine("Вставлено объектов: {0}", number);
            conn.Close();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).Update();
                }
            }
            this.Close();
        }
    }
}
