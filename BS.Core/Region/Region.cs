using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Region: BaseEntity
    {
        public string Name { get; set; }
        public Country Country { get; set; }
    }
}
