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
            public Students(bool isMentor = false)
            {
                InitializeComponent();

                menuItemStudent.Click += Student_Click;
                menuItemMentor.Click += Mentor_Click;

                GetMenu(isMentor);
                FillWithAllSubjects();
            }
        


            //
            //GUI
            //



            public void FillWithAllSubjects()
            {
                foreach (string subject in UserManagement.subjectList) // show every subject
                {
                    subjects_LstBx.Items.Add(subject);
                }
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

            public List<string> searchMatchingSubjects(string searchText)
            {
                List<string> matchList = new List<string>();

                foreach (string subject in UserManagement.subjectList)
                {
                    if (subject.ToLower().Contains(searchText.ToLower()))
                    {
                        matchList.Add(subject);
                    }
                }
                
                return matchList;
            }

            private void subjects_SelectionChanged(object sender, SelectionChangedEventArgs e)
            {
                if(appointments_LstBx.SelectedIndex != -1)
                {
                    UpdatePossibleAppointments();
                }
            }

            private void UpdatePossibleAppointments()
            {
                appointments_LstBx.Items.Clear();

                if (subjects_LstBx.SelectedItem != null)
                {
                    string selectedSubject = subjects_LstBx.SelectedItem.ToString();

                    List<Appointment> appointmentsFromSubject = UserManagement.LoadAppointmentsViaSubject(selectedSubject);

                    foreach (Appointment appo in appointmentsFromSubject)
                    {
                        foreach (Mentor m in UserManagement.mentors)
                        {
                            appointments_LstBx.Items.Add($"Mentor: {m.Name}, Von: {appo.StartTime} - {appo.EndTime.ToString("HH:mm")}, ID: {appo.Id}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Bitte wählen Sie ein Fach aus.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            private void UpdateBookedAppointments()
            {
                myAppointments_LstBx.Items.Clear();
                List<Appointment> myBookedAppointments = new List<Appointment>();

                foreach(Appointment a in UserManagement.appointments)
                {
                    if (a.Booker == UserManagement.localEmail)
                        myBookedAppointments.Add(a);
                }
                
                foreach(Appointment a in myBookedAppointments)
                {
                    myAppointments_LstBx.Items.Add($"Mentor: {a.Mentor}, Von: {a.StartTime} - {a.EndTime.ToString("HH:mm")}, ID: {a.Id}");
                }
            }



            //
            //APPOINTMENT
            //



        
            private void bookAppointment_Btn_Click(object sender, RoutedEventArgs e)
            {
                string appointmentID = appointments_LstBx.SelectedValue.ToString();
                appointmentID = appointmentID.Substring(appointmentID.LastIndexOf(';'), appointmentID.Length - appointmentID.LastIndexOf(';')).Trim();

                UserManagement.bookAppointment(int.Parse(appointmentID));

                UpdatePossibleAppointments();
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

                Appointment appointmentForDeletion = new Appointment();
                foreach (Appointment appo in UserManagement.appointments)
                {
                    if (appo.Id == int.Parse(IDfromSelected))
                    {
                        appointmentForDeletion = appo;
                    }
                }

                UserManagement.DeleteAppointment(appointmentForDeletion); // DB
                UserManagement.appointments.Remove(appointmentForDeletion); // C#

                UpdateBookedAppointments();
            }
            



            //
            //MENU
            //



            private void GetMenu(bool isMentor)
            {
                if (isMentor)
                    mainmenu.Items.Remove(menuItemStudent);
                else
                    mainmenu.Items.Remove(menuItemMentor);
            }

            private void Student_Click(object sender, RoutedEventArgs e)
            {
                MainWindow m = (MainWindow)Application.Current.MainWindow;
                m.application.Content = new Students();
            }

            private void Mentor_Click(object sender, RoutedEventArgs e)
            {
                UserManagement.LoadMentorsFromDB();
                Mentor localMentor = new Mentor();
                foreach(Mentor m in UserManagement.mentors)
                {
                    if(m.Email == UserManagement.localEmail)
                    {
                        localMentor = m;
                    }
                }
                if (localMentor.IsApproved)
                {
                    MainWindow m = (MainWindow)Application.Current.MainWindow;
                    m.application.Content = new MentorPage();
                }
                else
                    MessageBox.Show("Ein Administrator muss dich als Mentor bestätigen, um diese Seite aufrufen zu können!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            private void Information_Click(object sender, RoutedEventArgs e)
            {
                MessageBox.Show("Diese Mentoring-App ermöglicht es, dass Schüler zu Mentoren finden.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            private void Shutdown_Click(object sender, RoutedEventArgs e)
            {
                Application.Current.Shutdown();
            }
        }
    }
