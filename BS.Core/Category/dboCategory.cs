using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboCategory : dboBase
    {
        public CategoryType Type { get; set; }
        public List<Category> Categories { get; set; }
    }
}
