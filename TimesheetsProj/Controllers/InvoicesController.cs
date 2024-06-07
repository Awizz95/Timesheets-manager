using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Domain.ValueObjects;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[Action]")]
    public class InvoicesController : TimesheetBaseController
    {
        private readonly IInvoiceManager _invoiceManager;
        private readonly IContractManager _contractManager;

        public InvoicesController(IInvoiceManager invoiceManager, IContractManager contractManager)
        {
            _invoiceManager = invoiceManager;
            _contractManager = contractManager;
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Create([FromBody] InvoiceRequest invoiceRequest)
        {
            bool isAllowedToCreate;

            try
            {
                isAllowedToCreate = await _contractManager.CheckContractIsActive(invoiceRequest.ContractId);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

            if (!isAllowedToCreate)
            {
                return BadRequest($"Контракт {invoiceRequest.ContractId} не доступен.");
            }

            Guid id = await _invoiceManager.Create(invoiceRequest);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            Invoice result;

            try
            {
                result = await _invoiceManager.Get(id);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            string json = JsonSerializer.Serialize(result);

            return Ok(json);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Invoice> result;

            try
            {
                result = await _invoiceManager.GetAll();
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok(result);
        }

        [HttpPut("{invoiceId}")]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Update([FromRoute] Guid invoiceId, [FromBody] InvoiceRequest request)
        {
            bool isAllowedToUpdate;

            try
            {
                isAllowedToUpdate = await _contractManager.CheckContractIsActive(request.ContractId);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            if (!isAllowedToUpdate)
            {
                return BadRequest($"Контракт {invoiceId} не доступен.");
            }

            await _invoiceManager.Update(invoiceId, request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetTotalSum([FromQuery] Guid invoiceId)
        {
            Invoice invoice;
            Money money;

            try
            {
                invoice = await _invoiceManager.Get(invoiceId);
                money = await _invoiceManager.GetTotalSum(invoice);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            decimal sum = money.Amount;

            return Ok(sum);
        }

        [HttpGet]
        public async Task<IActionResult> CheckSum([FromQuery] Guid invoiceId)
        {
            Invoice invoice;

            try
            {
                invoice = await _invoiceManager.Get(invoiceId);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            Money money = _invoiceManager.CheckSum(invoice);
            decimal sum = money.Amount;

            return Ok(sum);
        }




    }
}
