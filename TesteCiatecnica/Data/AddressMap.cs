using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TesteCiatecnica.Models.Entities;

namespace TesteCiatecnica.Data
{
    public class AddressMap
    {
        public AddressMap(EntityTypeBuilder<Address> entityBuilder)
        {
            entityBuilder.Property("ZipCode")
                .IsRequired()
                .HasColumnType("VARCHAR(8)")
                .HasMaxLength(8);
            entityBuilder.Property("Street")
                .IsRequired()
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("Number")
                .IsRequired()
                .HasColumnType("VARCHAR(8)")
                .HasMaxLength(8);
            entityBuilder.Property("Complement")
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("Neighborhood")
                .IsRequired()
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("City")
                .IsRequired()
                .HasColumnType("VARCHAR(512)");
            entityBuilder.Property("State")
                .IsRequired()
                .HasColumnType("CHAR(2)")
                .HasMaxLength(2);
            entityBuilder
                .HasKey("AddressId");
            entityBuilder
                .ToTable("Addresses");
        }
    }
}
