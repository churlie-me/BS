using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class ServiceType : BaseEntity
    {
        public byte[] LightIcon { get; set; }
        public byte[] DarkIcon { get; set; }
        public string Type { get; set; }
        public List<Service> Services { get; set; }
    }
}
