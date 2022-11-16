using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayerProjectForJwt.Core.Entities;

namespace NLayerProjectForJwt.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Stock).IsRequired();
            builder.Property(c => c.Price).HasColumnType("decimal(18,2)");
            builder.Property(c => c.UserId).IsRequired();

            builder.ToTable("Products");
        }
    }
}
