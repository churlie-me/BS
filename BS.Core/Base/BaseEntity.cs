using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedOn { get; set; }

        public BaseEntity()
        {
            this.CreatedOn = DateTime.UtcNow;
        }
    }
}
