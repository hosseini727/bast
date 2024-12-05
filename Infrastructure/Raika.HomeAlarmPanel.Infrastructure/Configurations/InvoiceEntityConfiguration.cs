using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Raika.HomeAlarmPanel.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raika.HomeAlarmPanel.Infrastructure.Configurations
{
    public class DeviceHistoryEntityConfiguration : IEntityTypeConfiguration<DeviceHistory>
    {
        public void Configure(EntityTypeBuilder<DeviceHistory> builder)
        {
            builder.ToTable("DeviceHistory");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Brand)
                .HasMaxLength(100)
                .IsRequired(false);         
        }
    }
}
