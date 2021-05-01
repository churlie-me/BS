using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public byte[] Image { get; set; }
        public bool IsVisible { get; set; }
        public Guid AccountId { get; set; }
        [ForeignKey("AccountId")]
        public Account Account { get; set; }
    }
}
