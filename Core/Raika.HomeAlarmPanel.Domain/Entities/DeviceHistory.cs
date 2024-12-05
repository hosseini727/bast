using Raika.Common.SharedKernel.Interfaces;
using Raika.Common.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raika.HomeAlarmPanel.Domain.Enums;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Device;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.DeviceHistory;

namespace Raika.HomeAlarmPanel.Domain.Entities
{
    public class DeviceHistory : AuditableEntity<Guid>, IAggregateRoot
    {
        #region constructor
        public DeviceHistory(CreateDeviceHistoryParameterObject parameterObject)
        {
            IMEICode = parameterObject.IMEICode;
            DeviceType = parameterObject.DeviceType;
            Brand = parameterObject.Brand;
            Model = parameterObject.Model;
            Version = parameterObject.Version;
            FirstProductionSeries = parameterObject.FirstProductionSeries;
            ProductionSeries = parameterObject.ProductionSeries;
            SimCardNumber = parameterObject.SimCardNumber;
            UsersCellPhoneNumber = parameterObject.UsersCellPhoneNumber;
            ActivationCode = parameterObject.ActivationCode;
            CustomersId = parameterObject.CustomersId;
            CustomersName = parameterObject.CustomersName;
            TechniciansId = parameterObject.TechniciansId;
            TechniciansName = parameterObject.TechniciansName;
            TechnicianCellPhoneNumber = parameterObject.TechnicianCellPhoneNumber;
            TechnicianVertificationStatus = parameterObject.TechnicianVertificationStatus;
            RetailersCity = parameterObject.RetailersCity;
            StoreCode = parameterObject.StoreCode;
            InvoiceId = parameterObject.InvoiceId;
            InvoiceNumber = parameterObject.InvoiceNumber;
            RetailersId = parameterObject.RetailersId;
            RetailersName = parameterObject.RetailersName;
            TypeList = parameterObject.TypeList;
            StoreId = parameterObject.StoreId;
            LastOperation = parameterObject.LastOperation;
            LastOperationDate = parameterObject.LastOperationDate;
            GuarantyStatus = parameterObject.GuarantyStatus;
            BlockStatus = parameterObject.BlockStatus;
            HistoryId = parameterObject.HistoryId;
            ActivityTypes = parameterObject.ActivityTypes;
            OriginCausingActivity = parameterObject.OriginCausingActivity;
            ListLogsOrgin = parameterObject.ListLogsOrgin;
            RefrenceId = parameterObject.RefrenceId;
            ChangeDateTime = parameterObject.ChangeDateTime;
        }
        #endregion

        #region property
        public string IMEICode { get; private set; }
        public DeviceType DeviceType { get; private set; }
        public string Brand { get; private set; }
        public string Model { get; private set; }
        public string Version { get; private set; }
        public string FirstProductionSeries { get; private set; }
        public string ProductionSeries { get; private set; }
        public string SimCardNumber { get; private set; }
        public string UsersCellPhoneNumber { get; private set; }
        public string ActivationCode { get; private set; }
        public string CustomersId { get; private set; }
        public string CustomersName { get; private set; }
        public Guid TechniciansId { get; private set; }
        public string TechniciansName { get; private set; }
        public string TechnicianCellPhoneNumber { get; private set; }
        public bool TechnicianVertificationStatus { get; private set; }
        public string RetailersCity { get; private set; }
        public string StoreCode { get; private set; }
        public string InvoiceId { get; private set; }
        public string InvoiceNumber { get; private set; }
        public string RetailersId { get; private set; }
        public string RetailersName { get; private set; }
        public List<TypeList> TypeList { get; private set; }
        public string StoreId { get; private set; }
        public string LastOperation { get; private set; }
        public string LastOperationDate { get; private set; }
        public GuarantyStatus GuarantyStatus { get; private set; }
        public bool BlockStatus { get; private set; }
        public Guid HistoryId { get; private set; }
        public ActivityType ActivityTypes { get; private set; }
        public string OriginCausingActivity { get; private set; }
        public string ListLogsOrgin { get; private set; }
        public long RefrenceId { get; private set; }

        public DateTime ChangeDateTime { get; private set; }
        #endregion

        #region method
        public static DeviceHistory Create(CreateDeviceHistoryParameterObject parameterObject)
              => new(parameterObject);

        public void Update(UpdateDeviceHistoryParameterObject parameterObject)
        {
            IMEICode = parameterObject.IMEICode;
            DeviceType = parameterObject.DeviceType;
            Brand = parameterObject.Brand;
            Model = parameterObject.Model;
            Version = parameterObject.Version;
            FirstProductionSeries = parameterObject.FirstProductionSeries;
            ProductionSeries = parameterObject.ProductionSeries;
            SimCardNumber = parameterObject.SimCardNumber;
            UsersCellPhoneNumber = parameterObject.UsersCellPhoneNumber;
            ActivationCode = parameterObject.ActivationCode;
            CustomersId = parameterObject.CustomersId;
            CustomersName = parameterObject.CustomersName;
            TechniciansId = parameterObject.TechniciansId;
            TechniciansName = parameterObject.TechniciansName;
            TechnicianCellPhoneNumber = parameterObject.TechnicianCellPhoneNumber;
            TechnicianVertificationStatus = parameterObject.TechnicianVertificationStatus;
            RetailersCity = parameterObject.RetailersCity;
            StoreCode = parameterObject.StoreCode;
            InvoiceId = parameterObject.InvoiceId;
            InvoiceNumber = parameterObject.InvoiceNumber;
            RetailersId = parameterObject.RetailersId;
            RetailersName = parameterObject.RetailersName;
            TypeList = parameterObject.TypeList;
            StoreId = parameterObject.StoreId;
            LastOperation = parameterObject.LastOperation;
            LastOperationDate = parameterObject.LastOperationDate;
            GuarantyStatus = parameterObject.GuarantyStatus;
            BlockStatus = parameterObject.BlockStatus;
            HistoryId = parameterObject.HistoryId;
            ActivityTypes = parameterObject.ActivityTypes;
            OriginCausingActivity = parameterObject.OriginCausingActivity;
            ListLogsOrgin = parameterObject.ListLogsOrgin;
            RefrenceId = parameterObject.RefrenceId;
            ChangeDateTime = parameterObject.ChangeDateTime;
        }


        #endregion

    }
}
