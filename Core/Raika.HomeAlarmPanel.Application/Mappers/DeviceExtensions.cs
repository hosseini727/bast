using Raika.HomeAlarmPanel.Application.DTOs.DeviceDTOs;
using Raika.HomeAlarmPanel.Domain.Entities;


namespace Raika.HomeAlarmPanel.Application.Mappers
{
    public static class DeviceExtensions
    {
        public static DeviceSummaryDto ConvertToDeviceSummaryDto(this Device device)
        {
            return new DeviceSummaryDto()
            {
                DeviceId = device.Id,
                IMEICode = device.IMEICode,
                DeviceType = device.DeviceType,
                Brand = device.Brand,
                Model = device.Model,
                Version = device.Version,
                FirstProductionSeries = device.FirstProductionSeries,
                ProductionSeries = device.ProductionSeries,
                SimCardNumber = device.SimCardNumber,
                UsersCellPhoneNumber = device.UsersCellPhoneNumber,
                ActivationCode = device.ActivationCode,
                CustomersId = device.CustomersId,
                CustomersName = device.CustomersName,
                TechniciansId = device.TechniciansId,
                TechniciansName = device.TechniciansName,
                TechnicianCellPhoneNumber = device.TechnicianCellPhoneNumber,
                TechnicianVertificationStatus = device.TechnicianVertificationStatus,
                RetailersCity = device.RetailersCity,
                StoreCode = device.StoreCode,
                InvoiceId = device.InvoiceId,
                InvoiceNumber = device.InvoiceNumber,
                RetailersId = device.RetailersId,
                RetailersName = device.RetailersName,
                TypeList = device.TypeList,
                StoreId = device.StoreId,
                LastOperation = device.LastOperation,
                LastOperationDate = device.LastOperationDate,
                GuarantyStatus = device.GuarantyStatus,
                BlockStatus = device.BlockStatus
            };
        }


        public static List<DeviceSummaryDto> ConvertToDeviceSummaryDtos(this IEnumerable<Device> devices)
        {
            var result = new List<DeviceSummaryDto>();
            foreach (var device in devices)
                result.Add(device.ConvertToDeviceSummaryDto());
            return result;
        }
    }

}
