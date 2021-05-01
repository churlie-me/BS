using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboServiceType : dboBase
    {
        public bool ForReservation { get; set; }
        public List<ServiceType> Types { get; set; }
    }
}
