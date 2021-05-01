using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Job : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Guid BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public DateTime Deadline { get; set; }
        public List<Application> Applications { get; set; }
    }
}
