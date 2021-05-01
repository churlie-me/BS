using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BS.Core
{
    public class Account : IdentityUser<Guid>
    {
        public DateTime LastLogin { get; set; }
        public Guid CreationUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [NotMapped]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool? Active { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public AccountType Type { get; set; }
        public string VatNo { get; set; }
       /* public Guid? SeatId { get; set; }
        [ForeignKey("SeatId")]*/
        public virtual Seat Seat { get; set; }
        public List<AccountBranchService> AccountBranchServices { get; set; }
        public List<Request> Requests { get; set; }
        public List<Holiday> Holidays { get; set; }
        public Guid? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
    }
}
