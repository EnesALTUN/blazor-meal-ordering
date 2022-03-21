using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealOrdering.Server.Data.EntityFramework.Configuration;

public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder.ToTable("supplier");

        builder.HasKey(x => x.Id).HasName("pk_supplier_id");
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasColumnType("varchar")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(x => x.WebUrl)
            .HasColumnName("web_url")
            .HasColumnType("varchar")
            .HasMaxLength(2048)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .HasColumnType("boolean")
            .IsRequired();

        builder.Property(x => x.IsDeleted)
            .HasColumnName("is_deleted")
            .HasColumnType("boolean")
            .HasDefaultValue(false)
            .IsRequired();

        builder.Property(x => x.CreatedDate)
            .HasColumnName("created_date")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.Property(x => x.ModifiedDate)
            .HasColumnName("modified_date")
            .HasColumnType("timestamptz")
            .HasDefaultValueSql("now()")
            .IsRequired();
    }
}