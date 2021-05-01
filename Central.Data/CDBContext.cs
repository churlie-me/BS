using BS.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Central.Data
{
    public class CDBContext : IdentityDbContext<Account, Role, Guid>
    {
        public CDBContext(DbContextOptions<CDBContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Order> OrderItem { get; set; }
    }
}
