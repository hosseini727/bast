using Raika.HomeAlarmPanel.Domain.Enums;

namespace Raika.HomeAlarmPanel.Firewall.Api.Models.DeviceHistoryModels
{
    public class AddDeviceHistoryModel
    {
        public string IMEICode { get; set; }
        public DeviceType DeviceType { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string FirstProductionSeries { get; set; }
        public string ProductionSeries { get; set; }
        public string SimCardNumber { get; set; }
        public string UsersCellPhoneNumber { get; set; }
        public string ActivationCode { get; set; }
        public string CustomersId { get; set; }
        public string CustomersName { get; set; }
        public Guid TechniciansId { get; set; }
        public string TechniciansName { get; set; }
        public string TechnicianCellPhoneNumber { get; set; }
        public bool TechnicianVertificationStatus { get; set; }
        public string RetailersCity { get; set; }
        public string StoreCode { get; set; }
        public string InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string RetailersId { get; set; }
        public string RetailersName { get; set; }
        public List<TypeList> TypeList { get; set; }
        public string StoreId { get; set; }
        public string LastOperation { get; set; }
        public string LastOperationDate { get; set; }
        public GuarantyStatus GuarantyStatus { get; set; }
        public bool BlockStatus { get; set; }
        public long HistoryId { get; set; }
        public ActivityType ActivityTypes { get; set; }
        public string OriginCausingActivity { get; set; }
        public string ListLogsOrgin { get; set; }
        public long RefrenceId { get; set; }
        public DateTime ChangeDateTime { get; set; }
    }
}
