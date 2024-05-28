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

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] Guid id)
        {
            Sheet? result;

            try
            {
                result = await _sheetManager.Get(id);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
            
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Sheet>? result;

            try
            {
                result = await _sheetManager.GetAll();
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        /// <summary> Возвращает запись табеля </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SheetRequest sheet)
        {
            bool isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);

            if (!(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {sheet.ContractId} is not active or not found.");
            }

            Guid id = await _sheetManager.Create(sheet);

            return Ok(id);
        }

        /// <summary> Обновляет запись табеля </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] SheetRequest sheet)
        {
            bool isAllowedToCreate = await _contractManager.CheckContractIsActive(sheet.ContractId);

            if (!(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {sheet.ContractId} is not active or not found.");
            }

            await _sheetManager.Update(sheet);

            return Ok();
        }
    }
}
