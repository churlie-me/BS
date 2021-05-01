using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Page : BaseEntity
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public int Index { get; set; }
        public bool IsHomePage { get; set; }
        public bool ShowLinkInFooter { get; set; }
        public List<Row> Rows { get; set; }
        public Guid StoreId { get; set; }
    }
}
