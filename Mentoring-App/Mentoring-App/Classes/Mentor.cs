using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringClasses
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

    }
}
