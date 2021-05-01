using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboBranchRevenue
    {
        public Branch Branch { get; set; }
        public Guid BranchId { get; set; }
        public decimal Revenue { get; set; }
    }
}
