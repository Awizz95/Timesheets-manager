using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using TimesheetsProj.Domain.Managers.Implementation;
using TimesheetsProj.Domain.Managers.Interfaces;
using TimesheetsProj.Infrastructure.Extensions;
using TimesheetsProj.Models.Dto.Requests;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[Action]")]
    public class ContractController : TimesheetBaseController
    {
        private readonly IContractManager _contractManager;

        public ContractController(IContractManager contractManager)
        {
            _contractManager = contractManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetById([FromQuery] Guid id)
        {
            Contract result;

            try
            {
                result = await _contractManager.Get(id);
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
            IEnumerable<Contract> result;

            try
            {
                result = await _contractManager.GetAll();
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Client")]
        public async Task<IActionResult> Create([FromBody] ContractRequest request)
        {
            Guid id;

            try
            {
                request.EnsureNotNull(nameof(request));
                id = await _contractManager.Create(request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
                
            return Ok(id);
        }

        [HttpPut]
        [Authorize(Roles = "Admin, Client")]
        public async Task<IActionResult> Update([FromQuery] Guid contractId, [FromBody] ContractRequest request)
        {
            bool isAllowedToUpdate;

            try
            {
                isAllowedToUpdate = await _contractManager.CheckContractIsActive(contractId);
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            if (!isAllowedToUpdate)
            {
                return BadRequest($"Контракт: {contractId} не доступен.");
            }

            await _contractManager.Update(contractId, request);

            return Ok();
        }
    }
}
