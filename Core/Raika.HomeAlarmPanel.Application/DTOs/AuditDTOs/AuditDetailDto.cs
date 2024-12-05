using Raika.Common.SharedKernel.Enums;

namespace Raika.HomeAlarmPanel.Application.DTOs.AuditDTOs
{
    public class AuditDetailDto
    {
        public Guid UserId { get; set; }
        public Guid ApplicationId { get; set; }
        public string UserIp { get; set; }
        public string? DeviceType { get; set; }
        public string? BrowserName { get; set; }
        public string? PlatformName { get; set; }
        public string? EnginName { get; set; }
        public string? CrawlerName { get; set; }
        public DatabaseAuditTypeEnum AuditType { get; set; }
        public string? EntityName { get; set; }
        public string? EntitytId { get; set; }
        public string? ActionName { get; set; }
        public string? ControllerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Username { get; set; }
        public Dictionary<string, object> Changes { get; set; }
    }
}
