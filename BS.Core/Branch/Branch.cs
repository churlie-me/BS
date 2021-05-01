using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Branch : BaseEntity
    {
        public string Name { get; set; }
        public virtual Contact Contact { get; set; }
        public virtual Address Address { get; set; }
        public Guid StoreId { get; set; }
        public List<Seat> Seats { get; set; }
        public List<AccountBranchService> AccountBranchServices { get; set; }
        public List<Order> Orders { get; set; }
        public List<Account> Accounts { get; set; }
        public List<BranchService> BranchServices { get; set; }
    }
}
