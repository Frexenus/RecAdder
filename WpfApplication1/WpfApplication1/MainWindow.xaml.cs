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
        int CIing = 0;
        int CSteps = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ++CSteps;
            //adding step tag for xml
            StepsXML.Text += "\r\n" + "\t" + "<item>" + EachStep.Text + "</item>";
            EachStep.Clear();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //closing steps TAG for XMl
            CloseStepTAG.Background = Version.BorderBrush;
            StepsXML.Text += "\r\n" + "</string-array>" + "\r\n";
        }


        private void enName_KeyUp(object sender, KeyEventArgs e)
        {
            //delete tags
            toxml.Background = bigadd.Background;
            //set ENname for XML 
            XMLprogress.Value = 0;
            StepsXML.Text = "<string-array name=" + "\"" + enName.Text + "\"" + ">";
            numberofStepsXML.Text = "<string name=" + "\"" + "number_of_ingredients_for_" + enName.Text + "\"" + ">";

            if (enName.Text != "")
            {
                enName.BorderBrush = Version.BorderBrush;
            }
            else
            {
                enName.BorderBrush = EachStep.BorderBrush;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ++CIing;
            //adding element to ings for XML
            numberofStepsXML.Text += "\r\n" + "\t" + eachIngrid.Text + "\\n";
            eachIngrid.Clear();

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //closing ings TAG for XMl
            CloseIngsTAG.Background = Version.BorderBrush;
            numberofStepsXML.Text += "\r\n" +"</string>" + "\r\n";
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                DeleteLastLine(PathStep.Text);
                System.IO.File.AppendAllText(PathStep.Text, StepsXML.Text);
                System.IO.File.AppendAllText(PathStep.Text, "\r\n</resources>");
                XMLprogress.Value = 50;
                PathStep.BorderBrush = Version.BorderBrush;
            }
            catch (System.IO.FileNotFoundException)
            {
                PathStep.BorderBrush = Separator.Background;
            }

            try
            {
                DeleteLastLine(PathIngs.Text);
                System.IO.File.AppendAllText(PathIngs.Text, numberofStepsXML.Text);
                System.IO.File.AppendAllText(PathIngs.Text, "\r\n</resources>");
                XMLprogress.Value = 100;
                PathIngs.BorderBrush = Version.BorderBrush;
            }
            catch (System.IO.FileNotFoundException)
            {
                PathIngs.BorderBrush = Separator.Background;
                
            }
            
            //toxml.Background = XMLprogress.Foreground;
        }

        public static void DeleteLastLine(string filepath)
        {
                 List<string> lines = System.IO.File.ReadAllLines(filepath).ToList();

            System.IO.File.WriteAllLines(filepath, lines.GetRange(0, lines.Count - 1).ToArray());

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
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
              CSteps + ",'" +
              time.Text + "'," +
              Convert.ToInt32(persons.Text) +
              ",'number_of_ingredients_for_" + enName.Text + "', '" +
              CIing + "');", connection);

                SQLProcess.Value = 70;
                command.ExecuteNonQuery();
                SQLProcess.Value = 100;
                connection.Close();
                CIing = 0;
                CSteps = 0;
                CloseIngsTAG.Background = Separator.Background;
                CloseStepTAG.Background = Separator.Background;
                PathBase.BorderBrush = Version.BorderBrush;
            }
            catch (System.Data.SQLite.SQLiteException)
            {
                PathBase.BorderBrush = Separator.Background;
            }
            //string databaseName = @"C:\cyber.db";
            //SQLiteConnection.CreateFile(databaseName);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            name.Clear();
            enName.Clear();
            ingredients.Clear();
            time.Clear();
            persons.Clear();
            name.BorderBrush = EachStep.BorderBrush;
            time.BorderBrush = EachStep.BorderBrush;
            persons.BorderBrush = EachStep.BorderBrush;
            ingredients.BorderBrush = EachStep.BorderBrush;
            enName.BorderBrush = EachStep.BorderBrush;
        }

        private void name_KeyUp(object sender, KeyEventArgs e)
        {
            if (name.Text != "")
            {
                name.BorderBrush = Version.BorderBrush;
            }
            else
            {
                name.BorderBrush = EachStep.BorderBrush;
            }
        }

        private void time_KeyUp(object sender, KeyEventArgs e)
        {
            if (time.Text != "")
            {
                time.BorderBrush = Version.BorderBrush;
            }
            else
            {
                time.BorderBrush = EachStep.BorderBrush;
            }
        }

        private void persons_KeyUp(object sender, KeyEventArgs e)
        {
            if (persons.Text != "")
            {
                persons.BorderBrush = Version.BorderBrush;
            }
            else
            {
                persons.BorderBrush = EachStep.BorderBrush;
            }
        }

        private void ingredients_KeyUp(object sender, KeyEventArgs e)
        {
            string ingstring;
            ingstring = ingredients.Text;
            if (ingstring == null || ingstring == ""){
                return;
            }
            if (ingstring[ingstring.Length-1] == '.')
            {
                ingredients.BorderBrush = Version.BorderBrush;
            }
            else
            {
                ingredients.BorderBrush = EachStep.BorderBrush;
            }
        }
    }
}
