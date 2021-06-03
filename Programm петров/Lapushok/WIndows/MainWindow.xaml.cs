using Lapushok.User_Control;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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

namespace Lapushok
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

            internal void Load_data(string s)
            {
                outpanel.Children.Clear();
                using (SqlConnection connection = new SqlConnection(Lapushok.conn.conn.String))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($@"SELECT ProductType.Title, 
                                                              Product.Title AS Expr1, 
                                                              Product.ArticleNumber, 
                                                              Product.Image, 
                                                              Product.ProductionPersonCount,                                                               
                                                              Product.ProductionWorkshopNumber, 
                                                              Product.MinCostForAgent, 
                                                              Product.Description, 
                                                              MaterialType.Title AS Expr2, 
                                                              Material.Cost,
                                                              Product.ID
                                                       FROM   Product INNER JOIN
                                                              Material ON Product.ID = Material.ID INNER JOIN
                                                              ProductType ON Product.ProductTypeID = ProductType.ID INNER JOIN
                                                              MaterialType ON Material.MaterialTypeID = MaterialType.ID", connection);

                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            exit_info agent = new exit_info();
                            agent.Type.Content = reader[0];
                            agent.Name.Content = reader[1].ToString();
                            agent.Article.Content = reader[2];
                            try
                            {
                                agent.Photo.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "\\" + reader[3].ToString()));
                            }
                            catch
                            {
                            }
                            agent.Person.Content = reader[4];
                            agent.Number.Content = reader[5];
                            agent.Minimum.Content = reader[6];
                            agent.Description.Content = reader[7];
                            agent.Materials.Content = reader[8];
                            agent.Price.Content = reader[9];
                            agent.ID.Content = reader[10];
                            agent.Main = this;
                            outpanel.Children.Add(agent);
                        }
                    }
                }
            }

            private void Window_Loaded(object sender, RoutedEventArgs e)
            {
                Load_data("");
            }


        private void Left_Click(object sender, RoutedEventArgs e)
        {
            Sqroll.PageUp();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            Sqroll.PageDown();
        }

    }
    }


