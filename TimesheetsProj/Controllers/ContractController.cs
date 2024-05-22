using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TimesheetsProj.Domain.Managers.Interfaces;

namespace TimesheetsProj.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : TimesheetBaseController
    {
        private readonly IContractManager _contractManager;

        public ContractController(IContractManager contractManager)
        {
            _contractManager = contractManager;
        }


    }
}
