using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Holiday : BaseEntity
    {
        public string Name { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid? StoreId { get; set; }
        public Guid? AccountId { get; set; }
    }
}
