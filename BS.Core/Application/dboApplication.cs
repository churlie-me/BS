using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboApplication : dboBase
    {
        public List<Application> Applications { get; set; }
        public Guid JobId { get; set; }
    }
}
