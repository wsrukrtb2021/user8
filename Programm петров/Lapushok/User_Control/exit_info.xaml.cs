using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace Lapushok.User_Control
{

    public partial class exit_info : UserControl
    {
        public exit_info()
        {
            InitializeComponent();

        }
        public MainWindow Main;


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Lapushok.conn.conn.String))
            {
                if (MessageBox.Show("Удалить?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"Delete FROM Product WHERE ID = {ID.Content}", connection);
                    command.ExecuteNonQuery();
                    Main.Load_data("");
                }
                else
                {

                }
            }
        }
    }
}


