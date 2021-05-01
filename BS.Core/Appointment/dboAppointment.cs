using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboAppointment : dboBase
    {
        public Guid branchId { get; set; }     
        public string orderType { get; set; }   
        public List<Appointment> Appointments { get; set; }
        public Guid customerId { get; set; }
        public Guid stylistId { get; set; }
    }
}
