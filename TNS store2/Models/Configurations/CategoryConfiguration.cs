using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TNS_store2.Models.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);

            builder.HasData(new List<Category> 
            { 
                new Category() { Id = 1, Name = "Phones" }, 
                new Category() { Id = 2, Name = "Laptops" } 
            });
        }
    }
}