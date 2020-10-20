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
    /// Логика взаимодействия для ChangeName.xaml
    /// </summary>
    public partial class ChangeName : Window
    {
        public ChangeName()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            string _id = adid.Text.ToString();
            string _name = adna.Text.ToString();
            string _sname = adsu.SelectedDate.ToString();
            string ssql = $"UPDATE student_table SET Имя='{_id}', Фамилия='{_name}', Дата='{_sname}'";
            string connectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();// Открытие Соединения

            SqlCommand command = new SqlCommand(ssql, conn);
            int number = command.ExecuteNonQuery();
            Console.WriteLine("Обновлено объектов: {0}", number); // Выаолнение запроса обновления данных информации
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
