using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class AppointmentTime
    {
        public string Interval { get; set; }
        public DateTime DateTime { get; set; }
    }

    public class StylistSchedule
    {
        public Guid StylistId { get; set; }
        public List<AppointmentTime> AppointmentTimes { get; set; }
    }
}
