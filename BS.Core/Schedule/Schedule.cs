using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Schedule : BaseEntity
    {
        public DayOfWeek Day { get; set; }
        public OfficeStatus Status { get; set; }
        public DateTime OpenAt { get; set; }
        public DateTime ClosedAt{ get; set; }
    }
}
