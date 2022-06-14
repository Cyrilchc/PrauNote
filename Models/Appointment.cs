using System;
using System.Collections.Generic;

namespace Models
{
    public class Appointment
    {
        public int AppointmentId { get; set; }
        public string AppointmentTitle { get; set; }
        public DateTime StartDateAppointment { get; set; }
        public DateTime EndDateAppointment { get; set; }
        public Group Group { get; set; }
    }
}
