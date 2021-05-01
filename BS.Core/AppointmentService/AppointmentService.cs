using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class AppointmentService : BaseEntity
    {
        public Guid AppointmentId { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        public Guid? StylistId { get; set; }
        [ForeignKey("StylistId")]
        public Account Stylist { get; set; }
        public Guid? SeatId { get; set; }
        [ForeignKey("SeatId")]
        [NotMapped]
        public Seat Seat { get; set; }
    }
}
