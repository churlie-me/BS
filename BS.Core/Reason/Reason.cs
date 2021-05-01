using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Reason : BaseEntity
    {
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<Request> Requests { get; set; }
    }
}
