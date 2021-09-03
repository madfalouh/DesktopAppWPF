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
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;

namespace WpfApp2
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Employee> objList;


        public MainWindow()
        {
            InitializeComponent();
            objList = new List<Employee>();
            BindCountryDropDown();
            AddElementsInList();
           
             
          
            //SqlDataAdapter adapter = new SqlDataAdapter(cm);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            // mo.ItemsSource = dt.DefaultView;
            // mo.DisplayMemberPath = "FullName";

            //Console.WriteLine(  mo.Items.GetItemAt(0)); 
            

        }
        SqlConnection cnn = new SqlConnection(@"Data Source=DESKTOP-I443VPC;Initial Catalog=stage;Integrated Security=True");

        public object PublicParameters { get; }

        private void BindCountryDropDown()
        {
            mo.Items.Clear();
            mo.ItemsSource = objList; 
        }
        private void ddlCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ddlCountry_TextChanged(object sender, TextChangedEventArgs e)
        {
            mo.ItemsSource = objList.Where(x => x.Name.StartsWith(mo.Text.Trim()));
        }
        private void AllCheckbocx_CheckedAndUnchecked(object sender, RoutedEventArgs e)
        {
           
        }

      

       
        private void AddElementsInList()
        {
            // 1 element

            SqlCommand cm = new SqlCommand("select FullName from Employe", cnn);
            cnn.Open();
            SqlDataReader sqlRd = cm.ExecuteReader();
            Employee obj = new Employee();
            while (sqlRd.Read())
            {
                 obj = new Employee();
                Console.WriteLine(sqlRd["FullName"].ToString()); 
                obj.Name = sqlRd["FullName"].ToString();
                objList.Add(obj);
                 
            }

            cnn.Close(); 
           
        }



        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close(); 

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string emp = "";

            foreach (var i in objList)
            {
                if (i.Check_Status == true)
                {

                    emp = emp + i.Name.Trim() + ","   ; 
                }
            }


            Console.WriteLine(emp); 

            String a =  textBox.Text;
            String b = date.SelectedDate.Value.ToString("yyyy-MM-dd"); 
            Console.WriteLine(b); 
            String c = mo.Text;
            String d = em.Text;
            String client = textBox_Client.Text; 
            foreach (var i in objList)
            {
                if (i.Check_Status == true)
                {
                    SqlCommand cmd = new SqlCommand("insert into Previsions values( @projet , @date, @Employee ,@nb_mois  , @client )", cnn);

                    cmd.Parameters.AddWithValue("@projet", a);
                    cmd.Parameters.AddWithValue("@date", b);
                    cmd.Parameters.AddWithValue("@nb_mois", d);
                    cmd.Parameters.AddWithValue("@Employee", i.Name.Trim());
                    cmd.Parameters.AddWithValue("@client", client);

                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                }
            }
 
        }

        private void em_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void mo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }



    public class Employee
    {
         
        public string Name { get; set; }
        public Boolean Check_Status { get; set; }
    }


}
