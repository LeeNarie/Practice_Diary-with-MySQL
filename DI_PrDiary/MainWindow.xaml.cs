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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        public void Update()
        {
            string table = "diary_table";
            string connectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True";
            string ssql = $"SELECT * FROM {table} ";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);

            SqlDataAdapter dataAdp = new SqlDataAdapter(command);
            DataTable dt = new DataTable("diary_table"); 
            dataAdp.Fill(dt);
            Datgr.ItemsSource = dt.DefaultView;


            string table2 = "student_table";

            string ssql2 = $"SELECT * FROM {table2} ";

            SqlCommand command2 = new SqlCommand(ssql2, conn);

            SqlDataReader reader = command2.ExecuteReader(); // Выаолнение запроса вывод информации
            fio.Content = "";
            DateTime myDate = DateTime.Today;

            while (reader.Read()) //В цикле вывести всю информацию из таблици
            {

                 fio.Content += reader[0] + "\n" + reader[1] + "\n";
                myDate = DateTime.ParseExact(reader[2].ToString(), "dd.MM.yyyy H:mm:ss", null);
                fio.Content += myDate.ToString("D");
                 
            }

            var today = DateTime.Today;
            var age = today.Year - myDate.Year;
            if (myDate.Date > today.AddYears(-age)) age--;
            fio.Content += "(" + age + " лет)";

            conn.Close();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string table = "diary_table";
            string id = idin.Text;
            string ssql = $"DELETE  FROM diary_table  WHERE Дата= {id}";
            string connectionString = @"Data Source = .\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True";
            SqlConnection conn = new SqlConnection(connectionString); // Подключение к БД
            conn.Open();
            SqlCommand command = new SqlCommand(ssql, conn);

            int number = command.ExecuteNonQuery();
            Console.WriteLine("Удалено объектов: {0}", number); 
            Update();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
            AddRow Window = new AddRow();
            Window.Show();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Update();
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Update();
            ChangeDate Window = new ChangeDate();
            Window.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Update();
            ChangeName Window = new ChangeName();
            Window.Show();
        }
    }
}
