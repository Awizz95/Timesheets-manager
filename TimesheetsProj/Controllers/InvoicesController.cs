using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
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
            bool isAllowedToCreate = await _contractManager.CheckContractIsActive(invoiceRequest.ContractId);

            if (!isAllowedToCreate)
            {
                return BadRequest($"Contract {invoiceRequest.ContractId} is not active or not found.");
            }

            Guid id = await _invoiceManager.Create(invoiceRequest);

            return Ok(id);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid id)
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
                return BadRequest($"Contract {invoiceId} is not active or not found.");
            }

            await _invoiceManager.Update(invoiceId, request);

            return Ok();
        }
    }
}
