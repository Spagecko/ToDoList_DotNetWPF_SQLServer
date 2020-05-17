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
    /// Interaction logic for RemovePage.xaml
    /// </summary>
    public partial class RemovePage : Window
    {
        public RemovePage()
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
                CmdString = "SELECT * FROM Task_Table";

                SqlCommand cmd = new SqlCommand(CmdString, con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Tasks");

                sda.Fill(dt);

                deleteTaskGrid.ItemsSource = dt.DefaultView;

                con.Close();

            }

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;

         
            
            
            MessageBoxResult result =   MessageBox.Show("Are your sure you want to delete  ID ITEM : " + row_selected["id"] + "?","delete item", MessageBoxButton.YesNoCancel); 
         switch(result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("Id  deleted!", "delete item");
                    string ConString = Properties.Resources.ConnectionDB;

                    string CmdString = string.Empty;

                    using (SqlConnection con = new SqlConnection(ConString))

                    {
                        con.Open();



                        CmdString = "DELETE FROM " + "Task_Table" + " WHERE " + "Id" + " = '" + row_selected["Id"] + "'" 
                            +" AND "+ "TaskName" + " = '" + row_selected["TaskName"]+"'"
                            + " AND " + "DateStarted" + " = '" + row_selected["DateStarted"] + "'"
                            + " AND " + "ETA" + " = '" + row_selected["ETA"] + "'"
                            + " AND " + "CurrentState" + " = '" + row_selected["CurrentState"] + "'";

                        SqlCommand cmd = new SqlCommand(CmdString, con);
                        cmd.ExecuteNonQuery();
                        con.Close(); // close connection

                    }
                    RemovePage myWin = new RemovePage();
                    myWin.Show();
                    this.Close(); 

                    break; 
            }

        }


        private void Cancel_button_Click(object sender, RoutedEventArgs e) // return to main page on press
        {
            MainWindow myWin = new MainWindow();
            myWin.Show();
            this.Close(); 
        }
    }
}
