using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Row : BaseEntity
    {
        public int Index { get; set; }
        public byte[] BackgroundImage { get; set; }
        public byte[] BackgroundColor { get; set; }
        public ContentContainment ContentContainment { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaddingY { get; set; }
        public List<Column> Columns { get; set; }
        public Guid? PageId { get; set; }
    }
}
