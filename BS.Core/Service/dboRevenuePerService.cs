using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboRevenuePerService
    {
        public Service Service { get; set; }
        public int NoOfAppointments { get; set; }
        public decimal Revenue { get; set; }
    }
}
