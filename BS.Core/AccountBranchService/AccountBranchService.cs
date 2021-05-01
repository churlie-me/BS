using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class AccountBranchService : BaseEntity
    {
        public Guid AccountId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid ServiceId { get; set; }
        public Account Account { get; set; }
        public Branch Branch { get; set; }
        public Service Service { get; set; }
    }
}
