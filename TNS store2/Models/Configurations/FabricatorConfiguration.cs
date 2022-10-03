using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TNS_store2.Models.Configurations
{
    public class FabricatorConfiguration : IEntityTypeConfiguration<Fabricator>
    {
        public void Configure(EntityTypeBuilder<Fabricator> builder)
        {
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(1000);
        }
    }
}
