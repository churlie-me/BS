using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Order : BaseEntity
    {
        public Int16 OrderNo { get; set; }
        public DateTime OrderDate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OrderTotal { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public OrderType? OrderType { get; set; } = Core.OrderType.Product;
        public OrderStatus Status { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public Guid? AppointmentId { get; set; }
        [ForeignKey("AppointmentId")]
        public Appointment Appointment { get; set; }
    }
}
