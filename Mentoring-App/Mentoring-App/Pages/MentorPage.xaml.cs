using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaktionslogik für Mentor.xaml
    /// </summary>
    public partial class MentorPage : Page
    {
        public int ID = 5;
        public MentorPage()
        {
            InitializeComponent();

            LoadSubjectsInComboBox();
            LoadMentorSubjects();
            LoadAppointmentsToListbox();
        }



        //
        //GUI
        //



        private void LoadSubjectsInComboBox()
        {
            List<string> allSubjects = UserManagement.subjectList;
            foreach (string subject in allSubjects)
            {
                subjectComboBox.Items.Add(subject);
            }
        }

        private void LoadComboboxes(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 24; i++)
            {
                HoursComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 60; i += 5)
            {
                MinutesComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 24; i++)
            {
                Hours2ComboBox.Items.Add(i.ToString("00"));
            }

            for (int i = 0; i < 60; i += 5)
            {
                Minutes2ComboBox.Items.Add(i.ToString("00"));
            }
        }

        private void ClearAppointmentForum()
        {
            AppointmentDatePicker.SelectedDate = null;
            HoursComboBox.SelectedItem = null;
            MinutesComboBox.SelectedItem = null;
            Hours2ComboBox.SelectedItem = null;
            Minutes2ComboBox.SelectedItem = null;
        }



        //
        //SUBJECTS
        //



        private void LoadMentorSubjects()
        {
            foreach (Mentor m in UserManagement.mentors)
            {
                if (m.Email == UserManagement.localEmail)
                {
                    if (m != null && !string.IsNullOrEmpty(m.Subjects))
                    {
                        string[] subjectArray = m.Subjects.Split(';');

                        subjects.Items.Clear();

                        foreach (string subject in subjectArray)
                        {
                            subjects.Items.Add(subject.Trim());
                        }
                    }
                }
            }
            subjects.Items.RemoveAt(subjects.Items.Count - 1);
        }

        private void AddSubject_Click(object sender, RoutedEventArgs e)
        {
            if (subjectComboBox.SelectedItem != null)
            {
                string selectedSubject = subjectComboBox.SelectedItem as string;
                if (subjects.Items.Contains(selectedSubject))
                    MessageBox.Show("Du unterrichtest dieses Fach bereits!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                    subjects.Items.Add(selectedSubject);
            }
        }

        private void DeleteSubject_Click(object sender, RoutedEventArgs e)
        {
            if (subjects.SelectedItem != null)
            {
                if (subjects.Items.Count != 1)
                    subjects.Items.Remove(subjects.SelectedItem);
                else
                    MessageBox.Show("Du musst mindestens ein Fach unterrichten!","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        //
        //APPOINTMENTS
        //



        private void AddApointment_Click(object sender, RoutedEventArgs e)
        {
            DateTime selectedDate = AppointmentDatePicker.SelectedDate ?? DateTime.MinValue;
            selectedDate = DateTime.Parse(selectedDate.ToString("dddd, d M yyyy"));


            if (selectedDate != DateTime.MinValue)
            {
                int startHour = int.Parse(HoursComboBox.SelectedItem.ToString());
                int startMinute = int.Parse(MinutesComboBox.SelectedItem.ToString());

                int endHour = int.Parse(Hours2ComboBox.SelectedItem.ToString());
                int endMinute = int.Parse(Minutes2ComboBox.SelectedItem.ToString());

                DateTime startDateTime = selectedDate.Add(new TimeSpan(startHour, startMinute, 0));
                DateTime endDateTime = selectedDate.Add(new TimeSpan(endHour, endMinute, 0));

                Appointment newAppointment = new Appointment(" ", UserManagement.localEmail, ID.ToString(), startDateTime.ToString(), endDateTime.ToString(), "0", "0");

                ID++;

                UserManagement.AddAppointmentToDB(newAppointment);
                UserManagement.appointments.Add(newAppointment);
                ClearAppointmentForum();
                LoadAppointmentsToListbox();
            }
        }

        private void LoadAppointmentsToListbox()
        {
            myAppointments.Items.Clear();
            List<Appointment> myMentorAppointments = new List<Appointment>();
            foreach(Appointment a in UserManagement.appointments)
            {
                if(a.Mentor == UserManagement.localEmail)
                {
                    myMentorAppointments.Add(a);
                }
            }

            foreach(Appointment a in myMentorAppointments)
            {
                myAppointments.Items.Add($"Termin: {a.StartTime.ToString("HH:mm")} - {a.EndTime.ToString("HH:mm")}, ID: {a.Id}");
            }
        }

        private void AcceptAppointment_Click(object sender, RoutedEventArgs e)
        {
            if(myAppointments.SelectedItem != null) 
            {
                Appointment selectedAppointment = myAppointments.SelectedItem as Appointment;
                selectedAppointment.IsApproved = true;
                UserManagement.UpdateAppointment(selectedAppointment);
            }
        }

        private void DeclineAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (myAppointments.SelectedItem != null)
            {
                Appointment selectedAppointment = myAppointments.SelectedItem as Appointment;
                selectedAppointment.IsApproved = false;
                myAppointments.Items.Remove(selectedAppointment);
                myAppointments.Items.Refresh();
                UserManagement.DeleteAppointment(selectedAppointment);
            }
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Diese Mentoring-App ermöglicht es, dass Schüler zu Mentoren finden.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Shutdown_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ChangeToStudentPage_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = (MainWindow)Application.Current.MainWindow;
            m.application.Content = new Students(true);
        }
    }
}
