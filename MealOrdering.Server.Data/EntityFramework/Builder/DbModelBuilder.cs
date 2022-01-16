using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace MealOrdering.Server.Data.EntityFramework.Builder
{
    public class DbModelBuilder
    {
        public static ModelBuilder DbBuilder(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasKey(x => x.Id).HasName("pk_user_id");
                entity.HasIndex(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()").IsRequired();
                entity.Property(x => x.FirstName).HasColumnName("first_name").HasColumnType("varchar").HasMaxLength(100).IsRequired();
                entity.Property(x => x.LastName).HasColumnName("last_name").HasColumnType("varchar").HasMaxLength(100).IsRequired();
                entity.Property(x => x.EmailAddress).HasColumnName("email_address").HasColumnType("varchar").HasMaxLength(100).IsRequired();
                entity.Property(x => x.IsActive).HasColumnName("is_active").HasColumnType("boolean").IsRequired();
                entity.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean").HasDefaultValue(false).IsRequired();
                entity.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
                entity.Property(x => x.ModifiedDate).HasColumnName("modified_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
            });

            builder.Entity<Supplier>(entity =>
            {
                entity.ToTable("supplier");

                entity.HasKey(x => x.Id).HasName("pk_supplier_id");
                entity.HasIndex(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()").IsRequired();
                entity.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar").HasMaxLength(250).IsRequired();
                entity.Property(x => x.WebUrl).HasColumnName("web_url").HasColumnType("varchar").HasMaxLength(2048).IsRequired();
                entity.Property(x => x.IsActive).HasColumnName("is_active").HasColumnType("boolean").IsRequired();
                entity.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean").HasDefaultValue(false).IsRequired();
                entity.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
                entity.Property(x => x.ModifiedDate).HasColumnName("modified_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
            });

            builder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.HasKey(x => x.Id).HasName("pk_order_id");
                entity.HasIndex(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()").IsRequired();
                entity.Property(x => x.Name).HasColumnName("name").HasColumnType("varchar").HasMaxLength(250).IsRequired();
                entity.Property(x => x.Description).HasColumnName("description").HasColumnType("varchar").IsRequired();
                entity.Property(x => x.ExpireDate).HasColumnName("expire_date").HasColumnType("date").IsRequired();
                entity.Property(x => x.IsActive).HasColumnName("is_active").HasColumnType("boolean").IsRequired();
                entity.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean").HasDefaultValue(false).IsRequired();
                entity.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
                entity.Property(x => x.ModifiedDate).HasColumnName("modified_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
                entity.Property(x => x.CreatedUserId).HasColumnName("created_user_id").HasColumnType("uuid").IsRequired();
                entity.Property(x => x.SupplierId).HasColumnName("supplier_id").HasColumnType("uuid").IsRequired();

                entity.HasOne(order => order.CreateUser)
                    .WithMany(user => user.Orders)
                    .HasForeignKey(x => x.CreatedUserId)
                    .HasConstraintName("fk_order_user_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(order => order.Supplier)
                    .WithMany(supplier => supplier.Orders)
                    .HasForeignKey(x => x.SupplierId)
                    .HasConstraintName("fk_order_supplier_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<SubOrder>(entity =>
            {
                entity.ToTable("sub_order");

                entity.HasKey(x => x.Id).HasName("pk_sub_order_id");
                entity.HasIndex(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("id").HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()").IsRequired();
                entity.Property(x => x.Description).HasColumnName("description").HasColumnType("varchar").IsRequired();
                entity.Property(x => x.IsActive).HasColumnName("is_active").HasColumnType("boolean").IsRequired();
                entity.Property(x => x.IsDeleted).HasColumnName("is_deleted").HasColumnType("boolean").HasDefaultValue(false).IsRequired();
                entity.Property(x => x.CreatedDate).HasColumnName("created_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();
                entity.Property(x => x.CreatedUserId).HasColumnName("created_user_id").HasColumnType("uuid").IsRequired();
                entity.Property(x => x.OrderId).HasColumnName("order_id").HasColumnType("uuid").IsRequired();
                entity.Property(x => x.ModifiedDate).HasColumnName("modified_date").HasColumnType("date").HasDefaultValueSql("now()").IsRequired();

                entity.HasOne(subOrder => subOrder.Order)
                    .WithMany(order => order.SubOrders)
                    .HasForeignKey(x => x.OrderId)
                    .HasConstraintName("fk_sub_order_order_id")
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(subOrder => subOrder.CreatedUser)
                    .WithMany(user => user.SubOrders)
                    .HasForeignKey(x => x.CreatedUserId)
                    .HasConstraintName("fk_sub_order_user_id")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            return builder;
        }
    }
}
