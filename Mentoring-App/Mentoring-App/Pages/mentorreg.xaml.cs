using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using System.Xml.Linq;

namespace Mentoring_App.Pages
{
    /// <summary>
    /// Interaktionslogik für mentorreg.xaml
    /// </summary>
    public partial class mentorreg : Page
    {
        public static string username;
        public static string email;
        public static string password;
        public mentorreg()
        {
            InitializeComponent();
        }
        public static void GetUser(string u, string e, string p)
        {
            username = u;
            email = e;
            password = p;
        }

        private void mentorreg_Click(object sender, RoutedEventArgs e)
        {
            string subjects = "";
            if (german.IsChecked == true) { subjects += "german,"; }
            if (maths.IsChecked == true) { subjects += "maths,"; }
            if (english.IsChecked == true) { subjects += "english,"; }
            if (geography.IsChecked == true) { subjects += "geography,"; }
            if (chemistry.IsChecked == true) { subjects += "chemistry,"; }
            if (law.IsChecked == true) { subjects += "law,"; }
            if (networkEngineering.IsChecked == true) { subjects += "networkEngineering,"; }
            if (softwareDevelopment.IsChecked == true) { subjects += "softwareDevelopment,"; }
            if (mediaTechnology.IsChecked == true) { subjects += "mediaTechnology,"; }
            if (workshop.IsChecked == true) { subjects += "workshop,"; }
            if (itSecurity.IsChecked == true) { subjects += "itSecurity,"; }
            if (itProjects.IsChecked == true) { subjects += "itProjects,"; }
            if (databases.IsChecked == true) { subjects += "databases,"; }
            if (electricalEngineering.IsChecked == true) { subjects += "electricalEngineering,"; }
            if (systemTechnology.IsChecked == true) { subjects += "systemTechnology,"; }

            Mentor m = new Mentor(username, email, password, subjects, "false", grade.SelectedIndex.ToString());
            UserManagement.AddMentorToDB(m);
        }
    }
}
