using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core.Cart
{
    public class Cart : BaseEntity
    {
        public List<Article> Articles { get; set; }
    }
}
