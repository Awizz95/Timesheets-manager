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
        public async Task<ActionResult<IEnumerable<Service>>> GetAll()
        {
            IEnumerable<Service> result = await _serviceRepo.GetAll();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Service>> Get(Guid id)
        {
            Service? service;

            try
            {
                service = await _serviceRepo.Get(id);
            }
            catch (InvalidOperationException e)
            {
                return NotFound(e.Message);
            }

            return service;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Service service)
        {
            await _serviceRepo.Update(service);

            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Service>> Create([FromBody] string name)
        {
            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Name = name
            };

            await _serviceRepo.Create(service);

            return Ok(service);
        }

        public async Task<bool> ServiceExists(Guid id)
        {
            return await _serviceRepo.ServiceExists(id);
        }
    }
}
