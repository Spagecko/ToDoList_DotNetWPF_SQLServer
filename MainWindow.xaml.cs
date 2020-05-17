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

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TaskTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml


    public partial class MainWindow : Window
    {



        List<Task> myTasks = new List<Task>();
        public MainWindow()
        {
            InitializeComponent();
            FillDataGrid();
        }
        private void FillDataGrid()

        {
            string ConString = Properties.Resources.ConnectionDB;
            string CmdString = string.Empty;

            using (SqlConnection con = new SqlConnection(ConString))

            {
                con.Open();
                CmdString = "SELECT * FROM Task_Table"; // retrive current data 
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Tasks");
                sda.Fill(dt);
                grdTask.ItemsSource = dt.DefaultView;
                con.Close();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e) // add button
        {
            AddWindow myWin = new AddWindow();
            myWin.Show(); // opens up add new window    
            this.Close(); // closes this window ( main)
        }

        private void Button_Click_1(object sender, RoutedEventArgs e) //  remove button 
        {
            RemovePage myWin = new RemovePage();
            myWin.Show();
            this.Close();
        }

        private void grdTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //DEBUG
            //DataGrid gd = (DataGrid)sender;
            //DataRowView row_selected = gd.SelectedItem as DataRowView;
            //if (row_selected != null)
            //{
            //    DEBUG.Content = row_selected["Id"];
            //}
        }
    }

}
