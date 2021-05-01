using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboBase
    {
        public string search { get; set; }
        public string sortOrder { get; set; }
        public string column { get; set; }
        public int page { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public int pageCount { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
