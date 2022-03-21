using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealOrdering.Server.Data.EntityFramework.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("order");

        builder.HasKey(x => x.Id).HasName("pk_order_id");
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

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("varchar")
            .IsRequired();

        builder.Property(x => x.ExpireDate)
            .HasColumnName("expire_date")
            .HasColumnType("date")
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

        builder.Property(x => x.CreatedUserId)
            .HasColumnName("created_user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.SupplierId)
            .HasColumnName("supplier_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.HasOne(order => order.CreateUser)
            .WithMany(user => user.Orders)
            .HasForeignKey(x => x.CreatedUserId)
            .HasConstraintName("fk_order_user_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(order => order.Supplier)
            .WithMany(supplier => supplier.Orders)
            .HasForeignKey(x => x.SupplierId)
            .HasConstraintName("fk_order_supplier_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}