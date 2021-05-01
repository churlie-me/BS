using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class OrderItem: BaseEntity
    {
        public string Name { get; set; }
        public Guid? ArticleId { get; set; }
        [ForeignKey("ArticleId")]
        public Article Article { get; set; }
        public Guid? ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
