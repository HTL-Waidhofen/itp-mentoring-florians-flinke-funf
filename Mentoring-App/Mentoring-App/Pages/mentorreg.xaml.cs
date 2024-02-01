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
            if (german.IsChecked == true) { subjects += "Deutsch;"; }
            if (maths.IsChecked == true) { subjects += "Mathematik;"; }
            if (english.IsChecked == true) { subjects += "Englisch;"; }
            if (geography.IsChecked == true) { subjects += "Geografie,Geschichte,Politische Bildung;"; }
            if (chemistry.IsChecked == true) { subjects += "Naturwissenschaften;"; }
            if (law.IsChecked == true) { subjects += "Wirtschaft und Recht;"; }
            if (networkEngineering.IsChecked == true) { subjects += "Netzwerktechnik;"; }
            if (softwareDevelopment.IsChecked == true) { subjects += "Softwareentwicklung;"; }
            if (mediaTechnology.IsChecked == true) { subjects += "Medientechnik;"; }
            if (workshop.IsChecked == true) { subjects += "Computerpraktikum;"; }
            if (itSecurity.IsChecked == true) { subjects += "IT-Sicherheit;"; }
            if (itProjects.IsChecked == true) { subjects += "Informationstechnische Projekte;"; }
            if (databases.IsChecked == true) { subjects += "Informationssysteme;"; }
            if (electricalEngineering.IsChecked == true) { subjects += "Systemtechnik-E;"; }
            if (systemTechnology.IsChecked == true) { subjects += "Systemtechnik;"; }

            if (subjects == "")
                MessageBox.Show("Du musst mindestens ein Fach auswählen, welches du unterrichten möchtest", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
            {
                Mentor mentor = new Mentor(username, email, password, subjects, "1", (grade.SelectedIndex + 1).ToString());
                UserManagement.mentors.Add(mentor);
                UserManagement.AddMentorToDB(mentor);

                MainWindow m = (MainWindow)Application.Current.MainWindow;
                m.application.Content = new Students(true);
            }
        }
    }
}
