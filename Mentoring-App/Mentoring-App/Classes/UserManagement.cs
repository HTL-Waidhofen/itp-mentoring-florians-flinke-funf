﻿using System.Data.SQLite;
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



        //
        //CONNECTION STRING
        //



        private static string loadConnectionString()
        {
            return "DataSource=..\\..\\..\\DB\\MentoringDB.db;Version=3;";
        }



        //
        //LOAD
        //



        public static void LoadStudentsFromDB()
        {
            using (var con = new SQLiteConnection(loadConnectionString()))
            {
                con.Open();
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
                    }
                }
            }
        }

        public static void LoadMentorsFromDB()
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
                            Mentor mentor = new Mentor(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(5).ToString(), rdr.GetInt32(5).ToString());

                            if (!mentors.Any(m => m.Email == mentor.Email))
                            {
                                mentors.Add(mentor);
                                cnt++;
                            }
                        }
                    }
                }
            }
        }

        public static void LoadAppoinmentsFromDB()
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
                    }
                }
            }
        }



        //
        //ADD
        //



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



        //
        //DELETE
        //



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



        //
        //UPDATE
        //



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



        //
        //EMAIL & PASSWORD VALID
        //



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



        //
        //OTHERS
        //



        public static List<Appointment> LoadAppointmentsViaSubject(string selectedSubject)
        {
            List<Appointment> possibleAppointments = new List<Appointment> ();

            List<Mentor> possibleMentors = new List<Mentor>();

            foreach(Mentor m in mentors)
            {
                if(m.Subjects.Contains(selectedSubject))
                    possibleMentors.Add(m);
            }

            foreach(Appointment a in appointments)
            {
                foreach(Mentor m in possibleMentors)
                {
                    if(m.Email == a.Mentor && a.IsBooked == false)
                    {
                        possibleAppointments.Add(a);
                    }
                }
            }

            return possibleAppointments;
        }

        public static void bookAppointment(int appointmentID)
        {
            Appointment bookedAppointment = new Appointment();
            foreach(Appointment a in appointments)
            {
                if(a.Id == appointmentID)
                {
                    bookedAppointment = a;
                    bookedAppointment.IsBooked = true;
                    bookedAppointment.Booker = localEmail;
                }
            }
            UpdateAppointment(bookedAppointment);
        }

        public static string GetUserStatus(string email)
        {
            LoadStudentsFromDB();
            LoadMentorsFromDB();
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

        public static bool confirmLogin(string usermail, string password)
        {
            LoadStudentsFromDB();
            LoadMentorsFromDB();

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

    }
}
