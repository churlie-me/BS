using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboService : dboBase
    {
        public Guid CategoryId { get; set; }
        public Guid TypeId { get; set; }
        public List<Service> Services { get; set; }
    }
}
