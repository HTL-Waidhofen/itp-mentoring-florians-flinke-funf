using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_App
{
    public class Appointment
    {
        public Student Booker { get; set; }
        public Mentor Mentor { get; set; }
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsBooked { get; set; }
        public bool isApproved { get; set; }

        public Appointment(string booker, string mentor, string id, string startEndTime, string isBooked, string isApproved)
        {
            this.Booker = (Student)(from student in UserManagement.students
                          where student.Email == booker
                          select student);
            this.Mentor = (Mentor)(from ment in UserManagement.mentors
                                         where ment.Email == mentor
                                         select mentor);
            this.Id = int.Parse(id);
            this.StartTime = DateTime.Parse(startEndTime.Substring(0, startEndTime.IndexOf(';') - 1));
            this.EndTime = DateTime.Parse(startEndTime.Substring(startEndTime.IndexOf(';') + 1, startEndTime.Length - startEndTime.IndexOf(';') ));
            this.IsBooked = bool.Parse(isBooked);
            this.isApproved = bool.Parse(isApproved);
        }
    }
}
