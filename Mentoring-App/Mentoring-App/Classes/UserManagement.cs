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

        public static List<Mentor> LoadMentorsFromDB() // appointments werden nicht geladen
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

        public static void AddStudentToDB(Student student)
        {
            //using (var con = new SQLiteConnection(loadConnectionString()))
            //{
            //    con.Open();

            //    using (var cmd = new SQLiteCommand(con))
            //    {
            //        cmd.CommandText = "INSERT INTO Mitarbeiter(name, gehalt) VALUES(@name,@gehalt)";
            //        cmd.Parameters.AddWithValue("@name", person.name);
            //        cmd.Parameters.AddWithValue("@gehalt", person.gehalt);
            //        cmd.Prepare();
            //        cmd.ExecuteNonQuery();

            //    }
            //}

            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();

                using (var cmd = new SqliteCommand(con))
                {
                    cmd.CommandText = "INSERT INTO students(name, email, password) VALUES(@name,@email, @password)";
                    cmd.Parameters.AddWithValue("@name", student.Name);
                    cmd.Parameters.AddWithValue("@gehalt", student.Email);
                    cmd.Parameters.AddWithValue("@gehalt", student.Password);
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

        private static string loadConnectionString()
        {
            return "DataSource=MentoringDB.db;Version=3;";
        }
    }
}
