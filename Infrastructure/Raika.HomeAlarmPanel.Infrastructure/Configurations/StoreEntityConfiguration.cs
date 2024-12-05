using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raika.HomeAlarmPanel.Domain.Entities;

namespace Raika.HomeAlarmPanel.Infrastructure.Configurations
{
    public class StoreEntityConfiguration : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            //
            // Table definition
            //
            builder.ToTable("Store").HasKey(x => x.Id);

            //
            // Navigations
            //

            //
            // Properties
            //
        }
    }
}
