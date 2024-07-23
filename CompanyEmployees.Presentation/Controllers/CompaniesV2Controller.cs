using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [ApiVersion("2.0", Deprecated = true)]
    //[Route("api/{v:apiversion}/companies")]  // for URL versioning
    [Route("api/companies")]
    [ApiController]

    public class CompaniesV2Controller : ControllerBase
    {
        private readonly IServiceManager _service;
        public CompaniesV2Controller(IServiceManager service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetCompanies()
        {
            var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);

            //// for url versioning, modifying Name property to contain the V2 suffix just to see the difference
            //// in Postman by just inspection the response body
            //var companiesV2 = companies.Select(x => $"{x.Name} V2"); 
            //return Ok(companiesV2);

            return Ok(companies);
        }
    }
}


                            // THIS IS JUST FOR VERSIONING EXAMPLE'S SAKE
                            // THIS IS JUST FOR VERSIONING EXAMPLE'S SAKE
                            // THIS IS JUST FOR VERSIONING EXAMPLE'S SAKE