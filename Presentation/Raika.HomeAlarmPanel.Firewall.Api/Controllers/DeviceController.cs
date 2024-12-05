using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Raika.Common.SharedApplicationServices.Services;
using Raika.Common.SharedInfrastructure.DateTimeHelper;
using Raika.HomeAlarmPanel.ApiBase.Controllers;
using Raika.HomeAlarmPanel.Application.Services.DeviceHistoryServices.AddDeviceHistoryServices;
using Raika.HomeAlarmPanel.Application.Services.DeviceHistoryServices.UpdateDeviceHistoryServices;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.CreateDevice;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.DeleteDevice;
using Raika.HomeAlarmPanel.Application.Services.DeviceServices.Commands.UpdateDevice;
using Raika.HomeAlarmPanel.Application.Services.InvoiceServices.AddInvo;
using Raika.HomeAlarmPanel.Application.Services.InvoiceServices.AddInvoice;
using Raika.HomeAlarmPanel.Firewall.Api.Models.DeviceHistoryModels;
using Raika.HomeAlarmPanel.Firewall.Api.Models.DeviceModels;
using System.ComponentModel.DataAnnotations;
using Wangkanai.Detection.Services;

namespace Raika.HomeAlarmPanel.Firewall.Api.Controllers
{

    public class DeviceController : ApiBaseController
    {
        #region Constructor
        public DeviceController(
            IMediator mediator,
            IHttpContextAccessor contextAccessor,
            IDateTimeHelper dateTimeHelper,
            IDetectionService detectionService,
            ILogger<DeviceController> logger,
            ICurrentApplicationService currentApplicationService,
            ICurrentUserService currentUserService) : base(mediator, contextAccessor, dateTimeHelper, detectionService, logger, currentApplicationService, currentUserService)
        {

        }
        #endregion

        #region Device 
        [HttpPost("add")]
        public async Task<IActionResult> AddDevice([FromBody] AddDeviceModel model)
        {

            AddDeviceCommand command = new()
            {
                IMEICode = model.IMEICode,
                DeviceType = model.DeviceType,
                Brand = model.Brand,
                Model = model.Model,
                Version = model.Version,
                FirstProductionSeries = model.FirstProductionSeries,
                ProductionSeries = model.ProductionSeries,
                SimCardNumber = model.SimCardNumber,
                UsersCellPhoneNumber = model.UsersCellPhoneNumber,
                ActivationCode = model.ActivationCode,
                CustomersId = model.CustomersId,
                CustomersName = model.CustomersName,
                TechniciansId = model.TechniciansId,
                TechniciansName = model.TechniciansName,
                TechnicianCellPhoneNumber = model.TechnicianCellPhoneNumber,
                TechnicianVertificationStatus = model.TechnicianVertificationStatus,
                RetailersCity = model.RetailersCity,
                StoreCode = model.StoreCode,
                InvoiceId = model.InvoiceId,
                InvoiceNumber = model.InvoiceNumber,
                RetailersId = model.RetailersId,
                RetailersName = model.RetailersName,
                TypeList = model.TypeList,
                StoreId = model.StoreId,
                LastOperation = model.LastOperation,
                LastOperationDate = model.LastOperationDate,
                GuarantyStatus = model.GuarantyStatus,
                BlockStatus = model.BlockStatus
            };
            AddDeviceCommandValidator validator = new();
            await validator.ValidateAsync(command);

            var response = await _mediator.Send(command);
            if (!response.Success)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok();
        }


        [HttpPut("update/{deviceId}")]
        public async Task<IActionResult> UpdateDevice([FromBody] UpdateDeviceModel model)
        {
            UpdateDeviceCommand command = new()
            {
                IMEICode = model.IMEICode,
                DeviceType = model.DeviceType,
                Brand = model.Brand,
                Model = model.Model,
                Version = model.Version,
                FirstProductionSeries = model.FirstProductionSeries,
                ProductionSeries = model.ProductionSeries,
                SimCardNumber = model.SimCardNumber,
                UsersCellPhoneNumber = model.UsersCellPhoneNumber,
                ActivationCode = model.ActivationCode,
                CustomersId = model.CustomersId,
                CustomersName = model.CustomersName,
                TechniciansId = model.TechniciansId,
                TechniciansName = model.TechniciansName,
                TechnicianCellPhoneNumber = model.TechnicianCellPhoneNumber,
                TechnicianVertificationStatus = model.TechnicianVertificationStatus,
                RetailersCity = model.RetailersCity,
                StoreCode = model.StoreCode,
                InvoiceId = model.InvoiceId,
                InvoiceNumber = model.InvoiceNumber,
                RetailersId = model.RetailersId,
                RetailersName = model.RetailersName,
                TypeList = model.TypeList,
                StoreId = model.StoreId,
                LastOperation = model.LastOperation,
                LastOperationDate = model.LastOperationDate,
                GuarantyStatus = model.GuarantyStatus,
                BlockStatus = model.BlockStatus
            };
            var response = await _mediator.Send(command);
            if (!response.Success)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok();
        }
        #endregion

        #region DeviceHistory
        [HttpPost("add-history")]
        public async Task<IActionResult> AddDeviceHistory([FromBody] AddDeviceHistoryModel model)
        {
            AddDeviceHistoryCommand command = new()
            {
                IMEICode = model.IMEICode,
                DeviceType = model.DeviceType,
                Brand = model.Brand,
                Model = model.Model,
                Version = model.Version,
                FirstProductionSeries = model.FirstProductionSeries,
                ProductionSeries = model.ProductionSeries,
                SimCardNumber = model.SimCardNumber,
                UsersCellPhoneNumber = model.UsersCellPhoneNumber,
                ActivationCode = model.ActivationCode,
                CustomersId = model.CustomersId,
                CustomersName = model.CustomersName,
                TechniciansId = model.TechniciansId,
                TechniciansName = model.TechniciansName,
                TechnicianCellPhoneNumber = model.TechnicianCellPhoneNumber,
                TechnicianVertificationStatus = model.TechnicianVertificationStatus,
                RetailersCity = model.RetailersCity,
                StoreCode = model.StoreCode,
                InvoiceId = model.InvoiceId,
                InvoiceNumber = model.InvoiceNumber,
                RetailersId = model.RetailersId,
                RetailersName = model.RetailersName,
                TypeList = model.TypeList,
                StoreId = model.StoreId,
                LastOperation = model.LastOperation,
                LastOperationDate = model.LastOperationDate,
                GuarantyStatus = model.GuarantyStatus,
                BlockStatus = model.BlockStatus,
                ActivityTypes = model.ActivityTypes,
                OriginCausingActivity = model.OriginCausingActivity,
                ListLogsOrgin = model.ListLogsOrgin,
                RefrenceId = model.RefrenceId,
                ChangeDateTime = model.ChangeDateTime
            };

            AddDeviceHistoryCommandValidator validator = new();
            await validator.ValidateAsync(command);

            var response = await _mediator.Send(command);
            if (!response.Success)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok();
        }


        [HttpPut("update-history/{historyId}")]
        public async Task<IActionResult> UpdateDeviceHistory(Guid historyId, [FromBody] UpdateDeviceHistoryModel model)
        {
            UpdateDeviceHistoryCommand command = new()
            {
                HistoryId = historyId,
                IMEICode = model.IMEICode,
                DeviceType = model.DeviceType,
                Brand = model.Brand,
                Model = model.Model,
                Version = model.Version,
                FirstProductionSeries = model.FirstProductionSeries,
                ProductionSeries = model.ProductionSeries,
                SimCardNumber = model.SimCardNumber,
                UsersCellPhoneNumber = model.UsersCellPhoneNumber,
                ActivationCode = model.ActivationCode,
                CustomersId = model.CustomersId,
                CustomersName = model.CustomersName,
                TechniciansId = model.TechniciansId,
                TechniciansName = model.TechniciansName,
                TechnicianCellPhoneNumber = model.TechnicianCellPhoneNumber,
                TechnicianVertificationStatus = model.TechnicianVertificationStatus,
                RetailersCity = model.RetailersCity,
                StoreCode = model.StoreCode,
                InvoiceId = model.InvoiceId,
                InvoiceNumber = model.InvoiceNumber,
                RetailersId = model.RetailersId,
                RetailersName = model.RetailersName,
                TypeList = model.TypeList,
                StoreId = model.StoreId,
                LastOperation = model.LastOperation,
                LastOperationDate = model.LastOperationDate,
                GuarantyStatus = model.GuarantyStatus,
                BlockStatus = model.BlockStatus,
                ActivityTypes = model.ActivityTypes,
                OriginCausingActivity = model.OriginCausingActivity,
                ListLogsOrgin = model.ListLogsOrgin,
                RefrenceId = model.RefrenceId,
                ChangeDateTime = model.ChangeDateTime
            };
            UpdateDeviceHistoryCommandValidator validator = new();
            await validator.ValidateAsync(command);

            var response = await _mediator.Send(command);
            if (!response.Success)
                return StatusCode(StatusCodes.Status500InternalServerError);
            return Ok();
        }
        #endregion
    }
}
