using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboOrder : dboBase
    {
        public Guid UserId { get; set; }
        public Guid BranchId { get; set; }
        public OrderStatus Status { get; set; }
        public List<Order> Orders { get; set; }
    }
}
