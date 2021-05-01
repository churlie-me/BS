using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Village: BaseEntity
    {
        public virtual District District { get; set; }
        public string Name { get; set; }
        [MaxLength(50)]
        public string ZipCode { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal? Longitude { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal? Latitude { get; set; }
        public Guid DistrictId { get; set; }
    }
}
