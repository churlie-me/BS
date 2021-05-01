using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class dboAccount
    {
        public Account Account { get; set; }
        public string currentPassword { get; set; }
        public string newPassword { get; set; }
    }
}
