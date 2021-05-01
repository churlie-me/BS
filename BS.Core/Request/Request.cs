using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Request : BaseEntity
    {
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public Guid StoreId { get; set; }
        public DateTime RequestedOn { get; set; }
        public Guid ReasonId { get; set; }
        [ForeignKey("ReasonId")]
        public Reason Reason { get; set; }
    }
}
