using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.ToDoList.Data.Mappings
{
    public sealed class ItemMapping : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.HasKey(item => item.Id);

            builder.Property(item => item.Description)
                .IsRequired();

            builder.Property(item => item.Title)
                .IsRequired();

            builder.Property(item => item.Description)
                .IsRequired();

            builder.HasOne(item => item.User)
                .WithMany(user => user.Items)
                .HasForeignKey(user => user.UserId);

            builder.HasOne(user => user.List)
                .WithMany(list => list.Items)
                .HasForeignKey(item => item.ListId);

            builder.ToTable("Items");
        }
    }
}
