using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Raika.HomeAlarmPanel.Domain.Entities;
using Newtonsoft.Json;

namespace Raika.HomeAlarmPanel.Infrastructure.Configurations
{
    public class AuditLogEntityConfiguration : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            //
            // Table definition
            //
            builder.ToTable("AuditLog").HasKey(X => X.Id);

            //
            // Navigations
            //

            //
            // Properties
            //
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserIp).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.DeviceType).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.BrowserName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.PlatformName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.EnginName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.CrawlerName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.EntityName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.EntitytId).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.ActionName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.ControllerName).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");
            builder.Property(x => x.Changes)
                .HasConversion(value => JsonConvert.SerializeObject(value), serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));
            builder.Property(x => x.OldValues)
                .HasConversion(value => JsonConvert.SerializeObject(value), serializedValue => JsonConvert.DeserializeObject<Dictionary<string, object>>(serializedValue));
        }
    }
}
