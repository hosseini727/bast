using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedKernel;
using Raika.Common.SharedKernel.Interfaces;
using Raika.HomeAlarmPanel.Domain.Entities;
using Raika.HomeAlarmPanel.Domain.ParameterObjects;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Audit;

namespace Raika.HomeAlarmPanel.Infrastructure.DbContexts
{
    public class CommandDbContext : DbContext
    {
        private readonly IDomainEventDispatcher _dispatcher;
        private readonly ICurrentUserService _currentUserService;
        private readonly ICurrentApplicationService _currentApplicationService;

        public CommandDbContext(
           DbContextOptions<CommandDbContext> options,
           IDomainEventDispatcher dispatcher,
           ICurrentUserService currentUserService,
           ICurrentApplicationService currentApplicationService) : base(options)
        {
            _dispatcher = dispatcher;
            _currentUserService = currentUserService;
            _currentApplicationService = currentApplicationService;
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommandDbContext).Assembly);
        }

        private List<AuditLog> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var audits = new List<AuditLog>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State is EntityState.Detached || entry.State is EntityState.Unchanged || entry.Entity is AuditLog) continue;
                var audit = new CreateAuditLogParameterObject()
                {
                    AuditType = entry.State == EntityState.Added ? "Insert" : entry.State == EntityState.Deleted ? "Delete" : "Update",
                    EntitytId = entry.State == EntityState.Added ? null : entry.Properties.Single(x => x.Metadata.IsPrimaryKey()).CurrentValue?.ToString(),
                    EntityName = entry.Metadata.ClrType.Name,
                    UserId = _currentUserService.UserId,
                    ApplicationId = _currentApplicationService.ApplicationId,
                    Username = _currentUserService.Username,
                    CreatedAt = DateTime.Now,
                    UserIp = "",
                    Changes = entry.Properties.Where(x => !x.Metadata.IsPrimaryKey()).Select(p => new { p.Metadata.Name, p.CurrentValue }).ToDictionary(i => i.Name, i => i.CurrentValue)!,
                };
                foreach (var property in entry.Properties.Where(x => !x.Metadata.IsPrimaryKey()))
                {
                    var databaseValues = entry.GetDatabaseValues();
                    if (databaseValues == null) continue;
                    string propertyName = property.Metadata.Name;
                    audit.OldValues[propertyName] = databaseValues.GetValue<object>(propertyName)?.ToString() ?? null;
                }
                audits.Add(AuditLog.Create(audit));
            }
            AuditLogs.AddRange(audits);
            return audits;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            OnBeforeSaveChanges();
            foreach (var entry in ChangeTracker.Entries().Where(e => e.Entity is AuditableEntity<Guid>))
            {
                if (entry.Entity is not IDeletableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Deleted:
                            entry.Property("ModifiedBy").CurrentValue = _currentUserService.UserId;
                            entry.Property("ModifiedAt").CurrentValue = DateTime.Now;
                            entry.Property("IsDeleted").CurrentValue = true;
                            entry.State = EntityState.Modified;
                            break;
                        case EntityState.Modified:
                            entry.Property("ModifiedBy").CurrentValue = _currentUserService.UserId;
                            entry.Property("ModifiedAt").CurrentValue = DateTime.Now;
                            break;
                        case EntityState.Added:
                            entry.Property("CreatedBy").CurrentValue = _currentUserService.UserId;
                            entry.Property("CreatedAt").CurrentValue = DateTime.Now;
                            break;
                        default:
                            break;
                    }
                }
            }
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_dispatcher == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<AuditableEntity<Guid>>().Select(e => e.Entity).Where(e => e.DomainEvents.Any()).ToArray();
            await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
            return result;
        }
        public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();

        //
        // Audit logs
        //
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }

    }

}
