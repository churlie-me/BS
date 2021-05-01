using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Article: BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public byte[] Background { get; set; }
        public string Description { get; set; }
        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public Guid? BrandId { get; set; }
        [ForeignKey("BrandId")]
        public Brand Brand { get; set; }
        public SaleItem SaleItem { get; set; }
        public List<Instruction> Instructions { get; set; }
        public Guid? StoreId { get; set; }
    }
}
