using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class BranchService: BaseEntity
    {
        public Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public Guid ServiceId { get; set; }
        [ForeignKey("ServiceId")]
        public Service Service { get; set; }
    }
}
