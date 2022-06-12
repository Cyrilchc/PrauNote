using System;
using System.Collections.Generic;

namespace Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string AppointmentName { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public List<Group> Groups { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; }
    }
}
