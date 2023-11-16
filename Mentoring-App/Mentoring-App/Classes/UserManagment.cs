using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringClasses
{
    public class UserManagment
    {
        public List<Student> students = new List<Student>();
        public List<Mentor> mentors = new List<Mentor>();
        public List<Appointment> appointments = new List<Appointment>();
        

        public static List<Student> LoadStudentsFromDB()
        {
            using (var con = new SqliteConnection(loadConnectionString()))
            {
                con.Open();
                List<Student> students = new List<Student>();
                string stm = "SELECT * FROM student";

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

        List<Student> students1 = LoadStudentsFromDB();

        public void Test(List<Student> students)
        {
            foreach (Student s in students)
            {
                Console.WriteLine(s);
            }
        }

        private static string loadConnectionString()
        {
            return "DataSource=MentoringDB.db;Version=3;";
        }
    }
}
