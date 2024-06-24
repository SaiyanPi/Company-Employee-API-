using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyEmployees.Presentation.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly IServiceManager _service;
        // inject the IServiceManager interface inside the constructor.
        public CompaniesController(IServiceManager service) => _service = service;

        [HttpGet]
        public IActionResult GetCompanies()
        { 
            var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
            return Ok(companies);
        }

        // getting a single resource(company) from db
        [HttpGet("{id:guid}", Name = "CompanyById")]
        public IActionResult GetCompany(Guid id)
        {
            var company = _service.CompanyService.GetCompany(id, trackChanges: false);
            return Ok(company);
        }

        // POST
        [HttpPost]
        public IActionResult CreateCompany([FromBody] CompanyForCreationDto company)
        {
            if (company is null)
                return BadRequest("CompanyForCreationDto object is null");
            var createdCompany = _service.CompanyService.CreateCompany(company);

            //returns status code 201, also populate the body of the response with
            //the new company object as well as the location attribute within the
            //response header with the address to retrive that company.
            return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
        }

    }
}
