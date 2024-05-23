using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Models.Dto.Requests;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    public class ContractController : TimesheetBaseController
    {
        private readonly IContractManager _contractManager;

        public ContractController(IContractManager contractManager)
        {
            _contractManager = contractManager;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Guid id)
        {
            var result = _contractManager.GetItem(id);

            if (result is null) return NotFound();

            var json = JsonSerializer.Serialize(result);

            return Ok(json);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _contractManager.GetItems();

            if (result is null) return NotFound();

            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ContractRequest request)
        {
            var id = await _contractManager.Create(request);

            if (id == default) return BadRequest();

            return Ok(id);
        }


        [HttpPut("{contractId}")]
        public async Task<IActionResult> Update([FromRoute] Guid contractId, [FromBody] ContractRequest request)
        {
            var isAllowedToCreate = await _contractManager.CheckContractIsActive(contractId);

            if (!(bool)isAllowedToCreate)
            {
                return BadRequest($"Contract {contractId} is not active or not found.");
            }

            await _contractManager.Update(contractId, request);

            return Ok();
        }
    }
}
