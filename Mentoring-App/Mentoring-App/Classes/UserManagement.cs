using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using Mentoring_App.Pages;
using System.Windows.Controls;

namespace Mentoring_App
{
    public class UserManagement
    {
        public static List<Student> students = new List<Student>();
        public static List<Mentor> mentors = new List<Mentor>();
        public static List<Appointment> appointments = new List<Appointment>();
        public static string localEmail = "";

        public static List<string> subjectList = new List<string>() { "Deutsch", "Mathematik", "Englisch", "Geografie,Geschichte,Politische Bildung", "Naturwissenschaften", "Wirtschaft und Recht",
                                                            "Netzwerktechnik", "Softwareentwicklung", "Medientechnik", "Computerpraktikum", "IT-Sicherheit", "Informationstechnische Projekte",
                                                            "Informationssysteme", "Systemtechnik-E", "Systemtechnik", "Cloud Computing und industrielle Technologien"};


        // read
        public static List<Student> LoadStudentsFromDB()
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                List<Student> students = new List<Student>();
                string stm = "SELECT * FROM students";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Student st = new Student(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
                            students.Add(st);
                        }
                        return students;
                    }
                }
            }
        }

        public static List<Mentor> LoadMentorsFromDB()
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                string stm = "SELECT * FROM mentors";
                int cnt = 0;

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Mentor mentor = new Mentor(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetBoolean(4).ToString(), rdr.GetInt32(5).ToString());

                            if (!mentors.Any(m => m.Email == mentor.Email))
                            {
                                mentors.Add(mentor);
                                cnt++;
                            }
                        }
                        return mentors;
                    }
                }
            }
        }

        public static List<Appointment> LoadAppoinmentsFromDB()
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                string stm = "SELECT * FROM appointments";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Appointment appointment = new Appointment(rdr.GetString(0), rdr.GetString(1), rdr.GetInt32(2).ToString(), rdr.GetString(3), rdr.GetString(4), rdr.GetBoolean(5).ToString(), rdr.GetBoolean(6).ToString());
                            if (!appointments.Any(a => a.Id == appointment.Id))
                            {
                                appointments.Add(appointment);
                            }
                        }
                        return appointments;
                    }
                }
            }
        }

        public static void FillStudentMentorAppointments()
        {
            foreach (Student student in students)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (student.Email == appointment.Booker && !student.bookings.Contains(appointment))
                    {
                        student.bookings.Add(appointment);
                    }
                }
            }

            foreach (Mentor mentor in mentors)
            {
                foreach (Appointment appointment in appointments)
                {
                    if (mentor.Email == appointment.Mentor && !mentor.appointments.Contains(appointment))
                    {
                        mentor.appointments.Add(appointment);
                    }
                }
            }
        }

        // create
        public static void AddStudentToDB(Student student)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("INSERT INTO students(name, email, password) VALUES(@name,@email,@password)", con))
                {
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.Parameters.AddWithValue("@password", student.Password);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddMentorToDB(Mentor mentor)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("INSERT INTO mentors(name, email, password, subjects, isApproved, grade) VALUES(@name, @email, @password, @subjects, @isApproved, @grade)", con))
                {
                    cmd.Parameters.AddWithValue("@name", mentor.Name);
                    cmd.Parameters.AddWithValue("@email", mentor.Email);
                    cmd.Parameters.AddWithValue("@password", mentor.Password);
                    cmd.Parameters.AddWithValue("@subjects", mentor.Subjects);
                    cmd.Parameters.AddWithValue("@isApproved", Convert.ToInt32(mentor.IsApproved));
                    cmd.Parameters.AddWithValue("@grade", mentor.Grade);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static void AddAppointmentToDB(Appointment appointment)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("INSERT INTO appointments(booker, mentor, id, startTime, endTime, isBooked, isApproved) VALUES(@booker, @mentor, @id, @startTime, @endTime, @isBooked, @isApproved)", con))
                {
                    cmd.Parameters.AddWithValue("@booker", appointment.Booker);
                    cmd.Parameters.AddWithValue("@mentor", appointment.Mentor);
                    cmd.Parameters.AddWithValue("@id", appointment.Id);
                    cmd.Parameters.AddWithValue("@startTime", appointment.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", appointment.EndTime);
                    cmd.Parameters.AddWithValue("@isBooked", Convert.ToInt32(appointment.IsBooked));
                    cmd.Parameters.AddWithValue("@isApproved", Convert.ToInt32(appointment.IsApproved));
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // delete
        public static void DeleteStudent(Student student)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                string stm = "SELECT * FROM students";
                using (var cmd = new SQLiteCommand(stm, con))
                {
                    cmd.CommandText = "DELETE FROM students WHERE Email=@email;";
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteMentor(Mentor mentor)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                string stm = "SELECT * FROM mentors";
                using (var cmd = new SQLiteCommand(stm, con))
                {
                    cmd.CommandText = "DELETE FROM mentors WHERE Email=@email;";
                    cmd.Parameters.AddWithValue("@email", mentor.Email);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void DeleteAppointment(Appointment appoinment)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
                string stm = "SELECT * FROM appointments";
                using (var cmd = new SQLiteCommand(stm, con))
                {
                    cmd.CommandText = "DELETE FROM appointments WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@id", appoinment.Id);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Update

        public static void UpdateStudent(Student student)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("SELECT * FROM students", con))
                {
                    cmd.CommandText = "UPDATE students SET name=@name, password=@password WHERE email=@email;";
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@password", student.Password);
                    cmd.Parameters.AddWithValue("@email", student.Email);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateMentor(Mentor mentor)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("SELECT * FROM mentors", con))
                {
                    cmd.CommandText = "UPDATE mentors SET name=@name, password=@password, subjects=@subjects, isApproved=@isApproved, grade=@grade WHERE email=@email;";
                    cmd.Parameters.AddWithValue("@name", mentor.Name);
                    cmd.Parameters.AddWithValue("@password", mentor.Password);
                    cmd.Parameters.AddWithValue("@subjects", mentor.Subjects);
                    cmd.Parameters.AddWithValue("@isApproved", Convert.ToInt32(mentor.IsApproved));
                    cmd.Parameters.AddWithValue("@email", mentor.Email);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateAppointment(Appointment appointment)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("SELECT * FROM appointments", con))
                {
                    cmd.CommandText = "UPDATE appointments SET booker=@booker, mentor=@mentor, startTime=@startTime, endTime=@endTime, isBooked=@isBooked, isApproved=@isApproved WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@booker", appointment.Booker);
                    cmd.Parameters.AddWithValue("@mentor", appointment.Mentor);
                    cmd.Parameters.AddWithValue("@startTime", appointment.StartTime);
                    cmd.Parameters.AddWithValue("@endTime", appointment.EndTime);
                    cmd.Parameters.AddWithValue("@isBooked", Convert.ToInt32(appointment.IsBooked));
                    cmd.Parameters.AddWithValue("@isApproved", Convert.ToInt32(appointment.IsApproved));
                    cmd.Parameters.AddWithValue("@id", appointment.Id);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public static List<Appointment> GetMentorAppointments()
        {
            LoadStudentsFromDB();
            LoadMentorsFromDB();
            appointments = LoadAppoinmentsFromDB();
            List<Appointment> MentorAppointments = new List<Appointment>();
            foreach (var appointment in appointments)
            {
                if (appointment.Mentor == localEmail)
                {
                    MentorAppointments.Add(appointment);
                }
            }
            return MentorAppointments;
        }

        public static List<Appointment> GetAppointmentsFromSubject(string subject)
        {
            List<Appointment> subjectAppointments = new List<Appointment>();
            LoadAppoinmentsFromDB();

            foreach (Mentor mentor in mentors)
            {
                if (mentor.Subjects.Contains(subject))
                {
                    foreach (Appointment appointment in appointments)
                    {
                        if (mentor.Email == appointment.Mentor && !subjectAppointments.Contains(appointment))
                        {
                            subjectAppointments.Add(appointment);
                        }
                    }
                }
            }

            Appointment helperVar;

            for (int p = 0; p <= subjectAppointments.Count - 2; p++) // mithilfe von BubbleSort zeitlich sortieren
            {
                for (int i = 0; i <= subjectAppointments.Count - 2; i++)
                {
                    if (subjectAppointments[i].StartTime > subjectAppointments[i + 1].StartTime)
                    {
                        helperVar = subjectAppointments[i + 1];
                        subjectAppointments[i + 1] = subjectAppointments[i];
                        subjectAppointments[i] = helperVar;
                    }
                }
            }

            return subjectAppointments;
        }


        // test valid email

        public static bool IsEmailValid(string email)
        {
            foreach (Student s in students)
            {
                if (email == s.Email)
                {
                    return false;
                }
            }
            Regex regex = new Regex(@"(\w{1,50}\.?\w{1,50}@htlwy\.at)$");
            return (regex.IsMatch(email));
        }


        public static bool IsPasswordValid(string password, string confpassword)
        {
            if (password == confpassword && password.Length >= 4)
            {
                return true;
            }
            else return false;
        }

        // Register

        public static void StudentRegister(string name, string email, string password)
        {
            Student s = new Student(name, email, password);
            students.Add(s);
            AddStudentToDB(s);
        }
        private static string loadConnectionString()
        {
            return "DataSource=..\\..\\..\\bin\\Debug\\net6.0-windows\\MentoringDB.db;Version=3;";
        }
        public static bool confirmLogin(string usermail, string password)
        {
            students = LoadStudentsFromDB();
            mentors = LoadMentorsFromDB();

            foreach (var s in students)
            {
                if (s.Email == usermail && s.Password == password)
                {
                    return true;
                }
            }
            foreach (var m in mentors)
            {
                if (m.Email == usermail && m.Password == password)
                {
                    return true;
                }
            }
            return false;
        }
        public static string GetUserStatus(string email)
        {
            students = LoadStudentsFromDB();
            mentors = LoadMentorsFromDB();
            foreach (Mentor m in mentors)
            {
                if (m.Email == email)
                    return "mentor";
            }
            foreach (Student s in students)
            {
                if (s.Email == email)
                    return "student";
            }
            return "not available";
        }
        public static List<Mentor> GetApproved()
        {
            List<Mentor> myMentors = UserManagement.LoadMentorsFromDB();
            List<Mentor> approvedMentors = new List<Mentor>();
            foreach (Mentor m in myMentors)
            {
                if (m.IsApproved)
                {
                    approvedMentors.Add(m);
                }
            }
            return approvedMentors;
        }
        public static List<Mentor> GetAwaiting()
        {
            List<Mentor> myMentors = UserManagement.LoadMentorsFromDB();
            List<Mentor> awaitingMentors = new List<Mentor>();
            foreach (Mentor m in myMentors)
            {
                if (m.IsApproved == false)
                {
                    awaitingMentors.Add(m);
                }
            }
            return awaitingMentors;
        }

        // methods for students page

        public static Student GetLocalStudent(string localEmail)
        {
            foreach (Student student in students)
            {
                if (student.Email == localEmail)
                {
                    return student;
                }
            }

            return null;
        }

        public static void UpdateMyAppointmentsStudent(ListBox myAppointmentsListBox)
        {
            //METHODE
            LoadStudentsFromDB();
            LoadMentorsFromDB();
            LoadAppoinmentsFromDB();
            foreach (Appointment appo in GetLocalStudent(localEmail).bookings)
            {
                foreach (Mentor m in mentors)
                {
                    myAppointmentsListBox.Items.Add($"Mentor: {m.Name}, Von: {appo.StartTime} - {appo.EndTime.ToString("HH:mm")}, Angenommen: {(appo.IsApproved ? "Ja" : "Nein")}, ID: {appo.Id}");
                }
            }
        }

        public static void bookAppointmentStudent(ListBox appointmentsListBox, ListBox myAppointmentListBox)
        {
            if (appointmentsListBox.SelectedItem == null)
            {
                MessageBox.Show("Kein Termin ausgewählt", "Appointment-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (GetLocalStudent(localEmail) == null)
            {
                MessageBox.Show("User nicht gefunden", "User-Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Appointment Objekt bekommen anhand von ID 
            string tempSelectedItem = appointmentsListBox.SelectedItem.ToString();

            int idStartIndex = tempSelectedItem.IndexOf("ID: ") + 4;

            if (idStartIndex >= 0 && idStartIndex < tempSelectedItem.Length)
            {
                string IDfromSelected = tempSelectedItem.Substring(idStartIndex, Math.Min(10, tempSelectedItem.Length - idStartIndex));

                // Entferne mögliche Leerzeichen
                IDfromSelected = IDfromSelected.Trim();

                if (int.TryParse(IDfromSelected, out int parsedId))
                {
                    // Überprüfen Sie, ob die ID im gültigen Bereich liegt
                    if (parsedId > 0 && parsedId <= appointments.Count)
                    {
                        // Den ausgewählten Termin finden
                        Appointment selectedAppointment = appointments.FirstOrDefault(appo => appo.Id == parsedId);

                        if (selectedAppointment != null)
                        {
                            // Überprüfen, ob der Termin bereits gebucht ist
                            if (!selectedAppointment.IsBooked)
                            {
                                // Den Termin buchen
                                selectedAppointment.IsBooked = true;
                                selectedAppointment.Booker = GetLocalStudent(localEmail).Email;
                                UpdateAppointment(selectedAppointment);

                                // Weitere Aktualisierungen und Laden der Daten
                                LoadStudentsFromDB();
                                LoadMentorsFromDB();
                                LoadAppoinmentsFromDB();
                                FillStudentMentorAppointments();

                                // ListBox aktualisieren
                                appointmentsListBox.Items.Clear();
                                UpdateMyAppointmentsStudent(myAppointmentListBox);
                            }
                            else
                            {
                                MessageBox.Show("Der ausgewählte Termin ist bereits gebucht.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Termin nicht gefunden.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ungültiges ID-Format", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ungültiges ID-Format", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ungültiges Auswahlformat", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
