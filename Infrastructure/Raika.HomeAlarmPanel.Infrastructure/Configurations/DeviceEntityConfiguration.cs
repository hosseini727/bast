using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raika.HomeAlarmPanel.Domain.Entities;

namespace Raika.HomeAlarmPanel.Infrastructure.Configurations
{
    public class DeviceEntityConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.IMEICode)
                .HasMaxLength(100) 
                .IsRequired(false);           

            // builder.HasOne(d => d.Technician)
            //     .WithMany() 
            //     .HasForeignKey(d => d.TechniciansId);                     

        }
    }
}
