using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_App
{
    public class Mentor
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Subjects { get; set; }
        public bool IsApproved { get; set; }
        public int Grade { get; set; }

        public List<Appointment> appointments = new List<Appointment>();

        public Mentor(string name, string email, string password, string subjects, string isApproved, string grade) 
        {
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Subjects = subjects;
            if (isApproved == "0")
                this.IsApproved = false;
            else if (isApproved == "1")
                this.IsApproved = true;
            this.Grade = int.Parse(grade);
        }
    }
}
