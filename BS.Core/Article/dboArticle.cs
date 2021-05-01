using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboArticle: dboBase
    {
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
        public List<Article> Products { get; set; }
    }
}
