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
using System.Data.Common;
using System.Windows.Shapes;
using System.Data.SQLite;



namespace WpfApplication1
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //adding step tag for xml
            StepsXML.Text += "\r\n" + "\t" + "<item>" + EachStep.Text + "</item>";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //closing steps TAG for XMl
            StepsXML.Text += "\r\n" + "</string-array>" + "\r\n";
        }


        private void enName_KeyUp(object sender, KeyEventArgs e)
        {
            toxml.Background = bigadd.Background;
            //set ENname for XML 
            XMLprogress.Value = 0;
            StepsXML.Text = "<string-array name=" + "\"" + enName.Text + "\"" + ">";
            numberofStepsXML.Text = "<string name=" + "\"" + "number_of_ingredients_for_" + enName.Text + "\"" + ">";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //adding element to ings for XML
            numberofStepsXML.Text += "\r\n" + "\t" + eachIngrid.Text + "\\n";

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //closing ings TAG for XMl
            numberofStepsXML.Text += "\r\n" +"</string>" + "\r\n";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            DeleteLastLine(PathStep.Text);
            System.IO.File.AppendAllText(PathStep.Text, StepsXML.Text);
            System.IO.File.AppendAllText(PathStep.Text, "\r\n</resources>");
            XMLprogress.Value = 50;

            DeleteLastLine(PathIngs.Text);
            System.IO.File.AppendAllText(PathIngs.Text, numberofStepsXML.Text);
            System.IO.File.AppendAllText(PathIngs.Text, "\r\n</resources>");
            XMLprogress.Value = 100;
            //toxml.Background = XMLprogress.Foreground;
        }

        public static void DeleteLastLine(string filepath)
        {
                 List<string> lines = System.IO.File.ReadAllLines(filepath).ToList();

            System.IO.File.WriteAllLines(filepath, lines.GetRange(0, lines.Count - 1).ToArray());

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            string databaseName = PathBase.Text;
            SQLProcess.Value = 20;
            SQLiteConnection connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));
            SQLProcess.Value = 20;
            connection.Open();
            SQLProcess.Value = 40;
            //SQLiteCommand command = new SQLiteCommand("INSERT INTO 'Recipes' ('numberOfSteps') VALUES (1);", connection);
            SQLiteCommand command = new SQLiteCommand(@"INSERT INTO 'Recipes'
('name' , 'ingredients' , 'howToCook', 'numberOfSteps', 'timeForCooking', 'numberOfPersons' , 'numberOfEveryIng','numberOfIngredients')
VALUES ('" + name.Text + "','" + 
          ingredients.Text + "','" +
          enName.Text + "'," + 
          Convert.ToInt32(stepint.Text) + ",'" + 
          time.Text + "'," + 
          Convert.ToInt32(persons.Text) + 
          ",'number_of_ingredients_for_" + enName.Text + "', '" + 
          intIng.Text + "');", connection);

            SQLProcess.Value = 70;
            command.ExecuteNonQuery();
            SQLProcess.Value = 100;
            connection.Close();
        
            //string databaseName = @"C:\cyber.db";
            //SQLiteConnection.CreateFile(databaseName);
        }
    }
}
