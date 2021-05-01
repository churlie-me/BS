using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Contact Contact { get; set; }
        public Address Address { get; set; }
        public Guid RoleId { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<Order> Orders { get; set; }
        public List<Request> Requests { get; set; }
    }
}
