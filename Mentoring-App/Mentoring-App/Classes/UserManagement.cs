using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();
                List<Student> students = new List<Student>();
                string stm = "SELECT * FROM students";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();
                List<Mentor> mentors = new List<Mentor>();
                string stm = "SELECT * FROM mentors";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();
                List<Appointment> appointments = new List<Appointment>();
                string stm = "SELECT * FROM appointments";

                using (var cmd = new SqliteCommand(stm, con))
                {
                    using (SqliteDataReader rdr = cmd.ExecuteReader())
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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SqliteCommand("INSERT INTO students(name, email, password) VALUES(@name,@gehalt,@password)", con))
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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SqliteCommand("INSERT INTO mentors(name, email, password, subjects, isApproved, grade) VALUES(@name, @email, @password, @subjects, @isApproved, @grade)", con))
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
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SqliteCommand("INSERT INTO mentors(name, email, password, subjects, isApproved, grade) VALUES(@name, @email, @password, @subjects, @isApproved, @grade)", con))
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

        // Update



        //List<Student> students1 = LoadStudentsFromDB();

        //public void Test(List<Student> students)
        //{
        //    foreach (Student s in students)
        //    {
        //        Console.WriteLine(s);
        //    }
        //}

        private static string loadConnectionString()
        {
            return "DataSource=MentoringDB.db;Version=3;";
        }
    }
}
