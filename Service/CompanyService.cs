using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public CompanyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
        {          
            var companies = _repository.Company.GetAllCompanies(trackChanges);
            // mannual mapping
            // var companiesDto = companies.Select(c => new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country))).ToList();
                
            // using Automapper
            var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
            return companiesDto;

           
        }
        // getting a single resource(company) from db
        public CompanyDto GetCompany(Guid id, bool trackChanges)
        {
            var company = _repository.Company.GetCompany(id, trackChanges);          
            if (company is null)
                throw new CompanyNotFoundException(id);
            var companyDto = _mapper.Map<CompanyDto>(company);
            return companyDto;
        }

        //POST method
        public CompanyDto CreateCompany(CompanyForCreationDto company)
        {
            var companyEntity = _mapper.Map<Company>(company);
            _repository.Company.CreateCompany(companyEntity);
            _repository.Save();
            var companyToReturn = _mapper.Map<CompanyDto>(companyEntity);
            return companyToReturn;
        }

    }
}
