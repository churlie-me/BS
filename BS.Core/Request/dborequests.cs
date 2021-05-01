using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dborequests : dboBase
    {
        public List<Request> Requests { get; set; }
        public Guid ReasonId { get; set; }
    }
}
