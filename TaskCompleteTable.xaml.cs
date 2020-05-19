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
    /// Interaction logic for TaskCompleteTable.xaml
    /// </summary>
    public partial class TaskCompleteTable : Window
    {
        public TaskCompleteTable()
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
                CmdString = "SELECT * FROM Task_Table_Completed"; // retrive current data 
                SqlCommand cmd = new SqlCommand(CmdString, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable("Tasks");
                sda.Fill(dt);
                TaskCompleteGrid.ItemsSource = dt.DefaultView;
                con.Close();

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow myWin = new MainWindow();
            myWin.Show();
            this.Close(); 
             
        }

        private void TasCompleteGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
