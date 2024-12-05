using Raika.Common.SharedKernel.Interfaces;
using Raika.Common.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raika.HomeAlarmPanel.Domain.Enums;
using Raika.HomeAlarmPanel.Domain.ParameterObjects.Device;

namespace Raika.HomeAlarmPanel.Domain.Entities
{
    public class Device : AuditableEntity<Guid>, IAggregateRoot
    {

        #region Constructor    
        private Device(CreateDeviceParameterObject parameterObject)
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
        }

        #endregion

        #region Property
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
        public string TechniciansId { get; private set; }
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
        #endregion 

        #region Factory Method     

        public static Device Create(CreateDeviceParameterObject parameterObject)
            => new(parameterObject);

        public void Update(UpdateDeviceParameterObject parameterObject)
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
            GuarantyStatus = parameterObject.GuarantyStatus;
            BlockStatus = parameterObject.BlockStatus;
        }   
        #endregion Factory Method
    }
}
