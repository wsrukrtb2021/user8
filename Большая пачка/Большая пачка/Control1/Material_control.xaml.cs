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

namespace Большая_пачка.Control1
{
    /// <summary>
    /// Логика взаимодействия для Material_control.xaml
    /// </summary>
    public partial class Material_control : UserControl
    {
        public MainWindow main; // соед с МайнВиндов
        public Material_control()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Connecting.String)) // соединение к БД через класс
            {
                con.Open(); // открывает БД
                SqlCommand command = new SqlCommand($@"DELETE FROM Material where ID = {id.Content}", con); // запрос на удаление ИД в таблице Материал
                command.ExecuteNonQuery();
                main.Load_Data(""); // соед с МайнВиндов
            }
        }
    }
}
