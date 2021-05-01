using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Stylist: BaseEntity
    {
        public bool Active { get; set; }
        public Guid AccountId { get; set; }
        public Guid ServiceId { get; set; }

        [NotMapped]
        public Account Account { get; set; }
    }
}
