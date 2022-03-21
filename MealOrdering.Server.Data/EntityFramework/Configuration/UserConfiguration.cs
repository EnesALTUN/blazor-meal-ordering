using MealOrdering.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MealOrdering.Server.Data.EntityFramework.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("user");

        builder.HasKey(x => x.Id).HasName("pk_user_id");
        builder.HasIndex(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()")
            .IsRequired();

        builder.Property(x => x.FirstName)
            .HasColumnName("first_name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.LastName)
            .HasColumnName("last_name")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.EmailAddress)
            .HasColumnName("email_address")
            .HasColumnType("varchar")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Password)
            .HasColumnName("password")
            .HasColumnType("varchar")
            .HasMaxLength(100)
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