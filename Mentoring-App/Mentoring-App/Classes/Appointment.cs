﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring_App
{
    public class Appointment
    {
        public string Booker { get; set; }
        public string Mentor { get; set; }
        public int Id;
        public static int lastID = 0;

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsBooked { get; set; }
        public bool IsApproved { get; set; }


        public Appointment() { }
        public Appointment(string booker, string mentor, string id, string startTime, string endTime, string isBooked, string isApproved)
        {
            this.Booker = booker;
            this.Mentor = mentor;
            lastID += 1;
            this.Id = lastID + 1;
            this.StartTime = DateTime.Parse(startTime);
            this.EndTime = DateTime.Parse(endTime);
            if(isBooked == "0")
                this.IsBooked = false;
            else if (isBooked == "1")
                this.IsBooked = true;
            if (isApproved == "0")
                this.IsApproved = false;
            else if (isApproved == "1")
                this.IsApproved = true;
        }
        public override string ToString()
        {
            return StartTime.ToString().Substring(0, StartTime.ToString().Length - 3) + " - " + EndTime.TimeOfDay.Hours.ToString() + ":" + EndTime.TimeOfDay.Minutes.ToString() + ", " + Booker;
        }
    }
}
