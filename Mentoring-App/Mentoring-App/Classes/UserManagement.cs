using System.Data.SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows;
using Mentoring_App.Pages;

namespace Mentoring_App
{
    public class UserManagement
    {
        public static List<Student> students = new List<Student>();
        public static List<Mentor> mentors = new List<Mentor>();
        public static List<Appointment> appointments = new List<Appointment>();

        
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
                List<Mentor> mentors = new List<Mentor>();
                string stm = "SELECT * FROM mentors";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Mentor mentor = new Mentor(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5));
                            mentors.Add(mentor);
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
                List<Appointment> appointments = new List<Appointment>();
                string stm = "SELECT * FROM appointments";

                using (var cmd = new SQLiteCommand(stm, con))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Appointment appointment = new Appointment(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5));
                            appointments.Add(appointment);
                        }
                        return appointments;
                    }
                }
            }
        }

        public static void FillStudentMentorAppointments()
        {
            for (int i = 0;i < appointments.Count; i++)
            {
                for (int j = 0; j < students.Count; j++)
                {
                    if(students[j].Email == appointments[i].Booker.Email) students[j].bookings.Add(appointments[i]);
                }
            }
            
            for (int i = 0;i < appointments.Count; i++)
            {
                for (int j = 0; j < mentors.Count; j++)
                {
                    if(mentors[j].Email == appointments[i].Mentor.Email) mentors[j].appointments.Add(appointments[i]);
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
        public static void AddAppointmentToDB(Mentor mentor)
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

        public static void UpdateMentor(Appointment appointment)
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SQLiteCommand("SELECT * FROM appointments", con))
                {
                    cmd.CommandText = "UPDATE appointments SET booker=@booker, mentor=@mentor, startEndTime, isBooked=@isBooked, isApproved=@isApproved WHERE id=@id;";
                    cmd.Parameters.AddWithValue("@booker", appointment.Booker);
                    cmd.Parameters.AddWithValue("@mentor", appointment.Mentor);
                    cmd.Parameters.AddWithValue("@startEndTime", appointment.StartTime + ";" + appointment.EndTime);
                    cmd.Parameters.AddWithValue("@isBooked", Convert.ToInt32(appointment.IsBooked));
                    cmd.Parameters.AddWithValue("@isApproved", Convert.ToInt32(appointment.isApproved));
                    cmd.Parameters.AddWithValue("@id", appointment.Id);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        //List<Student> students1 = LoadStudentsFromDB();

        //public void Test(List<Student> students)
        //{
        //    foreach (Student s in students)
        //    {
        //        Console.WriteLine(s);
        //    }
        //}

        // test valid email

        public static bool IsEmailValid(string email) 
        {
            Regex regex = new Regex(@"(\w{1,50}.?\w{1,50}@htlwy.at)$");
            return(regex.IsMatch(email));
           
        }


        public static bool IsPasswordValid(string password, string confpassword)
        {
            if (password == confpassword)
            {
                return true;
            }
            else return false;
        }

        // Register

        public static void StudentRegister(string name, string email, string password)
        {
                Student s = new Student(name, email, password);
        }
        private static string loadConnectionString()
        {
            return "DataSource=MentoringDB.db;Version=3;";
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
    }
}
