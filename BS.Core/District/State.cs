using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class District: BaseEntity
    {
        public string Name { get; set; }
        public string ZipCode { get; set; }
        public Region Region { get; set; }
    }
}
