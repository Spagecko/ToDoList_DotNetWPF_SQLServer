using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Collections;
namespace TaskTracker
{

    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        public static string ConnectionString = Properties.Resources.ConnectionDB;


        private static Random random = new Random();
  
        public string generateRandomString()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);

            return finalString; 
        }





   
        public AddWindow()
        {
            InitializeComponent();
        }
        
       public bool validateData(string pTaskName,  string pStartedon, string pEta, string pStatus) // basic validatation for now 
        {
            
            if(String.IsNullOrEmpty(pTaskName)||  String.IsNullOrEmpty(pStartedon)  || String.IsNullOrEmpty(pEta)|| String.IsNullOrEmpty(pStatus) )  // if any are blank
            { return false; }


            return true; 
        }


        public void sendSQLData(string pTaskName, string pStartedon, string pEta, string pStatus)
        {

            string RandId = generateRandomString();
            string ConString = Properties.Resources.ConnectionDB;

            string CmdString = string.Empty;

            using (SqlConnection con = new SqlConnection(ConString))
            {
                Console.WriteLine("ETA");
                Console.WriteLine(pEta);
                Console.WriteLine("CURRENT STATE");
                Console.WriteLine(pStatus);
                con.Open();
                string temp = "temp"; // till i create user accounts 
                CmdString = " INSERT INTO New_Task_Table ( UserID, AssignedBy, TaskName, DateStarted, ETA, CurrentState  ) VALUES(@UserID, @AssignedBy, @TaskName, @DateStarted, @ETA, @CurrentState )";
                SqlCommand cmd = new SqlCommand(CmdString, con);
                cmd.Parameters.AddWithValue("@UserID", temp);
                cmd.Parameters.AddWithValue("@AssignedBy", temp);
                cmd.Parameters.AddWithValue("@TaskName", pTaskName);
                cmd.Parameters.AddWithValue("@DateStarted", pStartedon);
                cmd.Parameters.AddWithValue("@ETA", pEta);
                cmd.Parameters.AddWithValue("@CurrentState", pStatus);
                cmd.ExecuteNonQuery();
                con.Close(); // close connection

            }
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {// add button 
    
    
       
            string taskName = TaskName.Text;
            string startedOn = StartedOn.Text;
            string eta = ETA.Text;
            string status = Status.Text;
            string Id = "temp";
            string test = Properties.Resources.ConnectionDB;

            if (validateData(taskName, startedOn, eta, status))
            {
                DisplayBox.Text = "SUCESS!";
                sendSQLData(taskName,startedOn,eta,status); // send data to db 


            }
            else
            {
                DisplayBox.Text = "Please fill out the missing data";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e) // return to  make page
        {
            MainWindow newMainWindow = new MainWindow();
            newMainWindow.Show(); // opens up new window
            this.Close(); // closes this window 
        }

        private void DisplayBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
