using System;
using System.Collections.Generic;
using System.Text;

namespace BS.Core
{
    public class Address: BaseEntity
    {
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string Box { get; set; }
        public Guid? VillageId { get; set; }
        public virtual Village Village { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Guid? CountryId { get; set; }
        public virtual Country Country { get; set; }
    }
}
