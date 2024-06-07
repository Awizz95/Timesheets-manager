using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimesheetsProj.Data.Ef;
using TimesheetsProj.Data.Interfaces;
using TimesheetsProj.Models.Entities;

namespace TimesheetsProj.Controllers
{
    [Route("api/[controller]/[Action]")]
    [Authorize]
    [ApiController]
    public class ServicesController : TimesheetBaseController
    {
        private readonly IServiceRepo _serviceRepo;

        public ServicesController(IServiceRepo serviceRepo)
        {
            _serviceRepo = serviceRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            IEnumerable<Service> result;

            try
            {
                result = await _serviceRepo.GetAll();
            }
            catch(InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<Service>> GetById([FromQuery] Guid id)
        {
            Service service;

            try
            {
                service = await _serviceRepo.Get(id);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return Ok(service);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update([FromBody] Service service)
        {
            await _serviceRepo.Update(service);

            return Ok();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Employee")]
        public async Task<ActionResult<Service>> Create([FromBody] string name, decimal cost)
        {
            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Name = name,
                Cost = cost
            };

            await _serviceRepo.Create(service);

            return Ok(service);
        }

        [HttpGet]
        public async Task<IActionResult> ServiceExists([FromQuery] string name)
        {
            bool result = await _serviceRepo.ServiceExists(name);

            return Ok(result);
        }
    }
}
