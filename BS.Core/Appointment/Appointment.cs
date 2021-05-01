using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Appointment: BaseEntity
    {
        public DateTime AppointmentDate { get; set; }
        public Guid? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid? SeatId { get; set; }
        [ForeignKey("SeatId")]
        public Seat Seat { get; set; }
        public AppointmentStatus status { get; set; }
        public List<AppointmentService> Services { get; set; }
    }
}
