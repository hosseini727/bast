using Raika.Common.SharedKernel.Interfaces;
using Raika.Common.SharedKernel;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Audit;

namespace Raika.HomeAlarmPanel.Domain.Entities
{
    public class AuditLog : EntityBase<Guid>, IAggregateRoot
    {
        //
        // Constructors
        //
        private AuditLog()
        {

        }
        private AuditLog(CreateAuditLogParameterObject parameterObject)
        {
            ApplicationId = parameterObject.ApplicationId;
            UserId = parameterObject.UserId;
            UserIp = parameterObject.UserIp;
            DeviceType = parameterObject.DeviceType;
            BrowserName = parameterObject.BrowserName;
            PlatformName = parameterObject.PlatformName;
            EnginName = parameterObject.EnginName;
            CrawlerName = parameterObject.CrawlerName;
            AuditType = parameterObject.AuditType;
            EntityName = parameterObject.EntityName;
            EntitytId = parameterObject.EntitytId;
            ControllerName = parameterObject.ControllerName;
            ActionName = parameterObject.ActionName;
            CreatedAt = parameterObject.CreatedAt;
            Username = parameterObject.Username;
            Changes = parameterObject.Changes;
            OldValues = parameterObject.OldValues;
        }

        //
        // Factory Method
        //
        public static AuditLog Create(CreateAuditLogParameterObject parameterObject) => new(parameterObject);

        //
        // Entity Methods
        //

        //
        // Fields
        //        

        //
        // Properties
        //
        public Guid UserId { get; private set; }
        public Guid ApplicationId { get; private set; }
        public string UserIp { get; private set; }
        public string? DeviceType { get; private set; }
        public string? BrowserName { get; private set; }
        public string? PlatformName { get; private set; }
        public string? EnginName { get; private set; }
        public string? CrawlerName { get; private set; }
        public string AuditType { get; private set; }
        public string? EntityName { get; private set; }
        public string? EntitytId { get; private set; }
        public string? ActionName { get; private set; }
        public string? ControllerName { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? Username { get; private set; }
        public Dictionary<string, object> OldValues { get; private set; }
        public Dictionary<string, object> Changes { get; private set; }

        //
        // Navigation Properties
        //

        //
        // Validation
        //
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
