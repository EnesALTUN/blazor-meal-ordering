using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealOrdering.Server.Data.EntityFramework.Configuration;

public class SubOrderConfiguration : IEntityTypeConfiguration<SubOrder>
{
    public void Configure(EntityTypeBuilder<SubOrder> builder)
    {
        builder.ToTable("sub_order");

        builder.HasKey(x => x.Id).HasName("pk_sub_order_id");
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();

        builder.Property(x => x.Description)
            .HasColumnName("description")
            .HasColumnType("varchar")
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
            .HasColumnType("date")
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.Property(x => x.CreatedUserId)
            .HasColumnName("created_user_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.OrderId)
            .HasColumnName("order_id")
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(x => x.ModifiedDate)
            .HasColumnName("modified_date")
            .HasColumnType("date")
            .HasDefaultValueSql("now()")
            .IsRequired();

        builder.HasOne(subOrder => subOrder.Order)
            .WithMany(order => order.SubOrders)
            .HasForeignKey(x => x.OrderId)
            .HasConstraintName("fk_sub_order_order_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(subOrder => subOrder.CreatedUser)
            .WithMany(user => user.SubOrders)
            .HasForeignKey(x => x.CreatedUserId)
            .HasConstraintName("fk_sub_order_user_id")
            .OnDelete(DeleteBehavior.Cascade);
    }
}