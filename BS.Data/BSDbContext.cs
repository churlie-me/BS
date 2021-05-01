using BS.Core;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System;

namespace BS.Data
{
    public class BSDBContext : IdentityDbContext<Account, Role, Guid>
    {

        public BSDBContext(DbContextOptions<BSDBContext> options) : base(options) 
        {
        }

        public DbSet<Address> Address { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<Request> Request { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Reason> Reason { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<Branch> Branch { get; set; }
        public DbSet<Seat> Seat { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Stylist> Stylist { get; set; }
        public DbSet<Village> Village { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Region> Region { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<Instruction> Instruction { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Schedule> Schedule { get; set; }
        public DbSet<Holiday> Holiday { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Row> Row { get; set; }
        public DbSet<Column> Column { get; set; }
        public DbSet<Content> Content { get; set; }
        public DbSet<AccountBranchService> AccountBranchService { get; set; }
        public DbSet<Job> Job { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<BranchService> BranchService { get; set; }
        public DbSet<AppointmentService> AppointmentService { get; set; }
        public DbSet<ServiceType> ServiceType { get; set; }
        //public DbSet<Package> Package { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //AccountBranchService
            builder.Entity<AccountBranchService>() 
                .HasOne(u => u.Branch).WithMany(u => u.AccountBranchServices).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccountBranchService>()
                .HasOne(u => u.Service).WithMany(u => u.AccountBranchServices).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccountBranchService>()
                .HasKey(x => new { x.AccountId, x.BranchId, x.ServiceId });
            builder.Entity<AccountBranchService>()
                .HasOne(x => x.Account)
                .WithMany(m => m.AccountBranchServices)
                .HasForeignKey(x => x.AccountId);

            builder.Entity<Request>()
                .HasOne(x => x.Reason).WithMany(r => r.Requests).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Request>()
                .HasOne(x => x.User).WithMany(r => r.Requests).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Application>()
                .HasOne(x => x.Job).WithMany(r => r.Applications).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BranchService>()
                .HasOne(b => b.Branch)
                .WithMany(b => b.BranchServices).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Account>()
                        .HasOne(a => a.Seat)
                        .WithOne(b => b.Account)
                        .HasForeignKey<Seat>(b => b.AccountId);

            //Order No auto increments starting from 10000
            builder.HasSequence<int>("Order_seq", schema: "dbo")
                .StartsAt(10000)
                .IncrementsBy(1);

            builder.Entity<Order>()
                .Property(o => o.OrderNo)
                .HasDefaultValueSql("NEXT VALUE FOR dbo.Order_seq");
        }
    }
}
