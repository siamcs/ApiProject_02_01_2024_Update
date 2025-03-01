using ApiProject_02_01_2024.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiProject_02_01_2024.Data
{
    public class AppDBContext:DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Bank>? Banks { get; set; }  
        public DbSet<Designation> Designations { get; set; }
        public DbSet<HrmEmpDigitalSignature> HrmEmpDigitalSignatures { get; set; }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<CustomerDeliveryAddress>  CustomerDeliveryAddresses { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerDeliveryAddress>()
                .HasOne(cusDelAdd => cusDelAdd.Customer)
                .WithMany(c => c.CustomerDeliveryAddresses)
                .HasForeignKey(cusDelAdd => cusDelAdd.CustomerCode)
                .HasPrincipalKey(c => c.CustomerCode)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Customer>()
        .HasOne(cus => cus.CustomerType)
        .WithMany(ct => ct.Customers)
        .HasForeignKey(cus => cus.CusTypeCode)
        .HasPrincipalKey(ct => ct.CusTypeCode)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerType>().HasData(
              new CustomerType { Id = 1, CusTypeCode = "01", CustomerTypeName = "Dealer" },
              new CustomerType { Id = 2, CusTypeCode = "02", CustomerTypeName = "Retailer" },
              new CustomerType { Id = 3, CusTypeCode = "03", CustomerTypeName = "Corporate" },
              new CustomerType { Id = 4, CusTypeCode = "04", CustomerTypeName = "Export"  },
              new CustomerType { Id = 5, CusTypeCode = "05", CustomerTypeName = "Online" }
              );
        }
    }
}
