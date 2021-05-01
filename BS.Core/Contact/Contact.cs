using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Contact : BaseEntity
    {
        public string Phone { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string Fax { get; set; }
        public string Remarks { get; set; }
    }
}
