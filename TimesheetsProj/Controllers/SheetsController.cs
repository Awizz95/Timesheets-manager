using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]/[Action]")]
    public class SheetsController : TimesheetBaseController
    {
        private readonly ISheetManager _sheetManager;
        private readonly IContractManager _contractManager;

        public SheetsController(ISheetManager sheetManager, IContractManager contractManager)
        {
            _sheetManager = sheetManager;
            _contractManager = contractManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Guid sheetId)
        {
            Sheet result;

            try
            {
                result = await _sheetManager.Get(sheetId);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Sheet> result;

            try
            {
                result = await _sheetManager.GetAll();
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Create([FromBody] SheetRequest sheet)
        {
            bool isAllowedToCreate;

            try
            {
                isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (!isAllowedToCreate)
            {
                return BadRequest($"Контракт {sheet.ContractId} не доступен.");
            }

            Guid id = await _sheetManager.Create(sheet);

            return Ok(id);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> Update([FromQuery] Guid sheetId, [FromBody] SheetRequest request)
        {
            bool isAllowedToCreate;

            try
            {
                isAllowedToCreate = await _contractManager.CheckContractIsActive(request.ContractId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (!isAllowedToCreate)
            {
                return BadRequest($"Контракт {request.ContractId} не доступен.");
            }

            await _sheetManager.Update(sheetId, request);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Client")]
        public async Task<IActionResult> Approve([FromQuery] Guid sheetId, [FromBody] DateTime approveDate)
        {
            Sheet sheet;

            try
            {
                sheet = await _sheetManager.Get(sheetId);
                bool isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);

                if (!isAllowedToCreate)
                {
                    return BadRequest($"Контракт {sheet.ContractId} не доступен.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            await _sheetManager.Approve(sheet, approveDate);

            return Ok();
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<IActionResult> IncludeInvoice([FromQuery] Guid sheetId, Guid invoiceId)
        {
            await _sheetManager.IncludeInvoice(sheetId, invoiceId);

            return Ok();
        }
    }
}
