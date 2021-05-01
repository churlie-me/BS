using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Column : BaseEntity
    {
        public int Index { get; set; }
        public byte[] BackgroundImage { get; set; }
        public bool IsElevated { get; set; }
        public string BackgroundColor { get; set; }
        public List<Content> Contents { get; set; }
        public Guid RowId { get; set; }
    }
}
