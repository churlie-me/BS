using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Application : BaseEntity
    {
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        public byte[] Resume { get; set; }
        public Guid JobId { get; set; }
        [ForeignKey("JobId")]
        public Job Job { get; set; }
        public string Motivation { get; set; }
        public DateTime? SubmittedOn { get; set; }
    }
}
