using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Category: BaseEntity
    {
        public string Name { get; set; }
        public CategoryType Type { get; set; }
    }
}
