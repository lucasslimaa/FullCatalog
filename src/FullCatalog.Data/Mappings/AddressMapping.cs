using FullCatalog.Business;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FullCatalog.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Neighborhood)
                 .IsRequired()
                 .HasColumnType("varchar(200)");

            builder.Property(p => p.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(p => p.ZipCode)
                 .IsRequired()
                 .HasColumnType("varchar(8)");

            builder.Property(p => p.ApartmentNumber)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.City)
                 .IsRequired()
                 .HasColumnType("varchar(200)");

            builder.Property(p => p.State)
                 .IsRequired()
                 .HasColumnType("varchar(200)");


            builder.ToTable("Addresses");
        }
    }
}
