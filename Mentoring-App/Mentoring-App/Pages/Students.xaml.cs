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

namespace Mentoring_App.Pages
{
    /// <summary>
    /// Interaktionslogik für Students.xaml
    /// </summary>
    public partial class Students : Page
    {
        public Students(bool mentor = false)
        {
            InitializeComponent();

            UserManagement.UpdateMyAppointmentsStudent(myAppointments_LstBx);

            FillWithAllSubjects();
        }

        private void subjectSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            subjects_LstBx.Items.Clear();

            if (subjectSearch.Text == "") FillWithAllSubjects();

            foreach (string subject in searchMatchingSubjects(subjectSearch.Text))
            {
                subjects_LstBx.Items.Add(subject);
            }
        }            
        private void subjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            appointments_LstBx.Items.Clear();

            List<Appointment> appointmentsFromSubject = UserManagement.GetAppointmentsFromSubject(subjects_LstBx.SelectedItem.ToString()); // TExtbox ??
            foreach (Appointment appo in appointmentsFromSubject)
            {
                foreach(Mentor m in UserManagement.mentors)    
                {
                    appointments_LstBx.Items.Add($"Mentor: {m.Name}, Von: {appo.StartTime} - {appo.EndTime.ToString("HH:mm")}, Angenommen: {(appo.IsApproved ? "Ja" : "Nein")}, ID: {appo.Id}");
                }
            }
        }
        public List<string> searchMatchingSubjects(string searchText)
        {
            List<string> matchList = new List<string>();

            foreach (string subject in UserManagement.subjectList)
            {
                if (subject.Contains(searchText))
                {
                    matchList.Add(subject);
                }
            }

            return matchList;
        }
        public void FillWithAllSubjects()
        {
            foreach (string subject in UserManagement.subjectList) // show every subject
            {
                subjects_LstBx.Items.Add(subject);
            }
        }
        private void removeAppointment_Btn_Click(object sender, RoutedEventArgs e)
        {
            if (myAppointments_LstBx.SelectedItem == null)
            {
                MessageBox.Show("No Appointment selected", "Appointment-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Appointment Objekt bekommen anhand von ID 
            string tempSelectedItem = appointments_LstBx.SelectedItem.ToString();
            string IDfromSelected = tempSelectedItem.Substring(tempSelectedItem.IndexOf("ID: ") + 1, tempSelectedItem.Length - tempSelectedItem.IndexOf("ID: "));

            Appointment appointmentForDeletion = new Appointment("","","","","","","") ;
            foreach (Appointment appo in UserManagement.appointments)
            {
                if (appo.Id == int.Parse(IDfromSelected))
                {
                    appointmentForDeletion = appo;
                }
            }

            UserManagement.DeleteAppointment(appointmentForDeletion); // DB
            UserManagement.appointments.Remove(appointmentForDeletion); // C#
        }

        private void bookAppointment_Btn_Click(object sender, RoutedEventArgs e)
        {
            UserManagement.bookAppointmentStudent(appointments_LstBx, myAppointments_LstBx);

            GetMenu();
            GetMenu();

        }

        private void GetMenu()
        {
            MenuItem mi = new MenuItem();
            mi.Header = "Mentor";
            mi.Visibility = Visibility.Visible;
            mainmenu.Items.Add(mi);
            mainmenu.UpdateLayout();
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
