using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringClasses
{
    public class Appointment
    {
        public List<string> booker = new List<string>();
        public List<string> mentor = new List<string>();
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTIme { get; set; }

        public bool IsBooked { get; set; }
        public bool isApproved { get; set; }


    }
}
