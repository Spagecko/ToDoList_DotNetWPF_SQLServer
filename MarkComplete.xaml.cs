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
    /// Interaction logic for MarkComplete.xaml
    /// </summary>
    public partial class MarkComplete : Window
    {
        public MarkComplete()
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
                CmdString = "SELECT * FROM New_Task_Table";

                SqlCommand cmd = new SqlCommand(CmdString, con);

                SqlDataAdapter sda = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable("Tasks");

                sda.Fill(dt);

                MarkComplteGrid.ItemsSource = dt.DefaultView;

                con.Close();

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow myWin = new MainWindow();
            myWin.Show();
            this.Close();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            MessageBoxResult result = MessageBox.Show("Mark This Row Complte? : ", "Mark item Complete", MessageBoxButton.YesNoCancel);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("item Marked completed", "Item makred!");
                    string ConString = Properties.Resources.ConnectionDB;
                    string CmdString = string.Empty; 
                    using (SqlConnection con = new SqlConnection(ConString))
                    {
                        con.Open();
                        string temp = "temp"; // till i create user accounts 
                        CmdString = "DELETE FROM " + "New_Task_Table" + " WHERE " + "UserID" + " = '" + row_selected["UserID"] + "'"
                            + " AND " + "AssignedBy" + " = '" + row_selected["AssignedBy"] + "'"
                            + " AND " + "TaskName" + " = '" + row_selected["TaskName"] + "'"
                            + " AND " + "DateStarted" + " = '" + row_selected["DateStarted"] + "'"
                            + " AND " + "ETA" + " = '" + row_selected["ETA"] + "'"; // deleting  data from the old task table  
                        SqlCommand cmd = new SqlCommand(CmdString, con);
                        cmd.ExecuteNonQuery();
                        CmdString = " INSERT INTO Task_Table_Completed ( UserID, AssignedBy, TaskName, DateStarted, ETA, CurrentState  ) VALUES(@UserID, @AssignedBy, @TaskName, @DateStarted, @ETA, @CurrentState )"; // adding new data to the new task complete
                        cmd = new SqlCommand(CmdString, con);
                        cmd.Parameters.AddWithValue("@UserID", temp);
                        cmd.Parameters.AddWithValue("@AssignedBy", temp);
                        cmd.Parameters.AddWithValue("@TaskName", row_selected["TaskName"]);
                        cmd.Parameters.AddWithValue("@DateStarted", row_selected["DateStarted"]);
                        cmd.Parameters.AddWithValue("@ETA", row_selected["ETA"]);
                        cmd.Parameters.AddWithValue("@CurrentState", ""); // adding to the new table 
                        cmd.ExecuteNonQuery();
                        con.Close(); // close connection

                    }
                    RemovePage myWin = new RemovePage();
                    myWin.Show();
                    this.Close();

                    break;
            }
        }
    }
}
