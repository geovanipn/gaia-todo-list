using Gaia.ToDoList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gaia.ToDoList.Data.Mappings
{
    public sealed class ListMapping : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.Property(list => list.Title)
                .IsRequired();

            builder.HasMany(list => list.Items)
                .WithOne(item => item.List)
                .HasForeignKey(item => item.ListId);

            builder.HasOne(list => list.User)
                .WithMany(user => user.Lists)
                .HasForeignKey(list => list.UserId);

            builder.ToTable("Lists");
        }
    }
}
