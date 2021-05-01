using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Gender Gender { get; set; }
        public Guid? ServiceTypeId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Duration { get; set; }
        public Guid? SaleItemId { get; set; }
        [ForeignKey("SaleItemId")]
        public virtual SaleItem SaleItem { get; set; }
        public List<AccountBranchService> AccountBranchServices { get; set; }
        public Guid CreatedBy { get; set; }
        public List<Appointment> Appointments { get; set; }
        public List<BranchService> BranchServices { get; set; }
    }
}
