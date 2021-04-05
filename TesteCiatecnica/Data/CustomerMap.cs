using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteCiatecnica.Models.Entities;

namespace TesteCiatecnica.Data
{
    public class CustomerMap
    {   
        public CustomerMap(EntityTypeBuilder<Customer> entityBuilder)
        {
            entityBuilder.Property("CustomerType")
                .IsRequired();
            entityBuilder.Property("SSNorEIN")
                .IsRequired()
                .HasColumnType("VARCHAR(14)")
                .HasMaxLength(14);
            entityBuilder.Property("NameOrCompanyName")
                .IsRequired()
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("LastNameOrTradingName")
                .IsRequired()
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("BirthDate")
                .HasColumnType("DATE");
            entityBuilder
                .HasKey("CustomerId");
            entityBuilder
                .ToTable("Customers");
        }
    }
}
