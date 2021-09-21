using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.ToDoList.Data.Mappings
{
    public sealed class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(user => user.Id);

            builder.Property(user => user.Name)
                .IsRequired();

            builder.Property(user => user.Email)
                .IsRequired();

            builder.Property(user => user.Login)
                .IsRequired();

            builder.Property(user => user.Password)
                .IsRequired();

            builder
                .HasMany(user => user.Lists)
                .WithOne(list => list.User)
                .HasForeignKey(list => list.UserId);

            builder.ToTable("Users");
        }
    }
}
