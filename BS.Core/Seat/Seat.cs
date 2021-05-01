using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Seat : BaseEntity
    {
        public string Name { get; set; }
        public Guid? AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public Guid? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}
