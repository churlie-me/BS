using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Store : BaseEntity
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Latitude { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Longitude { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TextColor { get; set; }
        public string FooterBackgroundColor { get; set; }
        public string FooterTextColor { get; set; }
        public string HeaderBackgroundColor { get; set; }
        public string HeaderTextColor { get; set; }
        public byte[] Logo { get; set; }
        public byte[] StoreImage { get; set; }
        public Guid AccountId { get; set; }
        public List<Page> Pages { get; set; }
        public List<Service> Services { get; set; }
        public virtual List<Schedule> Schedules { get; set; }
        public virtual List<Branch> Branches { get; set; }
        public string Url { get; set; }
        public string AndroidAppUrl { get; set; }
        public string IosAppUrl { get; set; }
        public string SenderEmail { get; set; }
        public string SenderEmailPassword { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public bool IsSslEnabled { get; set; }
        public string StoreDesc { get; set; }
    }
}
