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
using System.Data.SqlClient;
using Большая_пачка.Class1;
using Большая_пачка.Control1;
using System.IO;

namespace Большая_пачка
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load_Data(""); //соед с Load_Data
        }

        internal void Load_Data(string a)
        {
            spisok_material.Children.Clear(); // очистить Врап панель который выводит данные 
            using (SqlConnection con = new SqlConnection(Connecting.String)) // Connecting - соед с классом для подключения БД
            {
                con.Open(); //открыть БД (позже закроется)
                SqlCommand com = new SqlCommand($@"SELECT TOP (1000) [ID]
                                                 ,[Title]
                                                 ,[CountInPack]
                                                 ,[Unit]
                                                 ,[CountInStock]
                                                 ,[MinCount]
                                                 ,[Description]
                                                 ,[Cost]
                                                 ,[Image]
                                                 ,[MaterialTypeID]
                                                 FROM [user8].[dbo].[Material] where Title like '{search.Text}%'" + a, con); // запрос БД на открытие tabl и search сортировка по Title
                SqlDataReader reader = com.ExecuteReader(); //Загрузить
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Material_control cont = new Material_control(); //открывает юз контрол
                        cont.id.Content = reader[0];
                        cont.Title_and_Type.Content = reader[1];
                        cont.CountInPack.Content = @"Остаток: " + reader[2];
                        cont.Unit.Content = reader[3];
                        cont.CountLnStock.Content = reader[4];
                        cont.MinCount.Content = @"Минимальное количество: " + reader[5];
                        cont.Description.Content = reader[6];
                        cont.Cost.Content = reader[7];
                        cont.image.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[8])); // автоматически ищет путь в bin\debag
                        cont.main = this; // соединение с контролом
                        spisok_material.Children.Add(cont); // добавить в Врап панель который выводит данные 
                    }
                }
            }
        }

        private void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Load_Data(""); //соед с Load_Data

        }

        private void sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filtr != null)
            {
                Load_Data(""); //соед с Load_Data
            }
        }
    }
}
