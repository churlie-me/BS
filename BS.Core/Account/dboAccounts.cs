using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboAccounts : dboBase
    {
        public AccountType Type { get; set; }
        public Guid? BranchId { get; set; }
        public List<Account> Accounts { get; set; }
    }
}
