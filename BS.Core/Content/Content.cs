using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public byte[] Image { get; set; }
        public bool IsRounded { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal BorderRadius { get; set; }
        public string TextColor { get; set; }
        public string Align { get; set; }
        public string Text { get; set; }
        public string FontSize { get; set; }
        public string Url { get; set; }
        public string UrlText { get; set; }
        public ContentType ContentType { get; set; }
        public int Index { get; set; }
        public Guid ColumnId { get; set; }
        //In height
        [Column(TypeName = "decimal(18,2)")]
        public decimal Space { get; set; }
        public virtual Row Row { get; set; }
    }
}
