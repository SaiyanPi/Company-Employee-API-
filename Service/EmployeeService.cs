using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal sealed class EmployeeService : IEmployeeService
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        public EmployeeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        //Extracting common code
        private async Task CheckIfCompanyExists(Guid companyId, bool trackChanges)
        {
            var company = await _repository.Company.GetCompanyAsync(companyId,
            trackChanges);
            if (company is null)
                throw new CompanyNotFoundException(companyId);
        }
        private async Task<Employee> GetEmployeeForCompanyAndCheckIfItExists(Guid companyId, Guid id, bool trackChanges)
        {
            var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            if (employeeDb is null)
                throw new EmployeeNotFoundException(id);
            return employeeDb;
        }
        //

        //public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId, bool trackChanges)
        //public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync(Guid companyId,
        //    EmployeeParameter employeeParameters, bool trackChanges)
        //after final improvement in paging
        public async Task<(IEnumerable<EmployeeDto> employees, MetaData metaData)> GetEmployeesAsync
            (Guid companyId, EmployeeParameter employeeParameters, bool trackChanges)
        {
            if (!employeeParameters.ValidAgeRange)
                throw new MaxAgeRangeBadRequestException();

            //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, trackChanges);
            //var employeesFromDb = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
            //var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);
            //return employeesDto;
            var employeesWithMetaData = await _repository.Employee.GetEmployeesAsync(companyId, employeeParameters, trackChanges);
            var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesWithMetaData);
            return (employees: employeesDto, metaData: employeesWithMetaData.MetaData);
        }

        // getting a single resource(employee) from db
        public async Task<EmployeeDto> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges)
        {
            //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, trackChanges);
            //var employeeDb = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            //if (employeeDb is null)
            //    throw new EmployeeNotFoundException(id);
            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);
            var employee = _mapper.Map<EmployeeDto>(employeeDb);
            return employee;
        }

        public async Task<EmployeeDto> CreateEmployeeForCompanyAsync(Guid companyId, 
            EmployeeForCreationDto employeeForCreation, bool trackChanges)
        {
            //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, trackChanges);
            var employeeEntity = _mapper.Map<Employee>(employeeForCreation);
            _repository.Employee.CreateEmployeeForCompany(companyId, employeeEntity);
            await _repository.SaveAsync();
            var employeeToReturn = _mapper.Map<EmployeeDto>(employeeEntity);
            return employeeToReturn;
        }

        public async Task DeleteEmployeeForCompanyAsync(Guid companyId, Guid id, bool trackChanges)
        {
            //var company = await _repository.Company.GetCompanyAsync(companyId, trackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, trackChanges);

            //var employeeForCompany = await _repository.Employee.GetEmployeeAsync(companyId, id, trackChanges);
            //if (employeeForCompany is null)
            //    throw new EmployeeNotFoundException(id);
            //_repository.Employee.DeleteEmployee(employeeForCompany);
            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, trackChanges);
            _repository.Employee.DeleteEmployee(employeeDb);
            await _repository.SaveAsync();
        }

        public async Task UpdateEmployeeForCompanyAsync(Guid companyId, Guid id, EmployeeForUpdateDto employeeForUpdate,
        bool compTrackChanges, bool empTrackChanges)
        {
            //var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, compTrackChanges);

            //var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            //if (employeeEntity is null)
            //    throw new EmployeeNotFoundException(id);
            //_mapper.Map(employeeForUpdate, employeeEntity);
            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);
            _mapper.Map(employeeForUpdate, employeeDb);
            await _repository.SaveAsync();
        }

        //patch
        public async Task <(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)> GetEmployeeForPatchAsync
        (Guid companyId, Guid id, bool compTrackChanges, bool empTrackChanges)
        {
            //var company = await _repository.Company.GetCompanyAsync(companyId, compTrackChanges);
            //if (company is null)
            //    throw new CompanyNotFoundException(companyId);
            await CheckIfCompanyExists(companyId, compTrackChanges);

            //var employeeEntity = await _repository.Employee.GetEmployeeAsync(companyId, id, empTrackChanges);
            //if (employeeEntity is null)
            //    throw new EmployeeNotFoundException(companyId);
            var employeeDb = await GetEmployeeForCompanyAndCheckIfItExists(companyId, id, empTrackChanges);

            //var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeEntity);
            //return (employeeToPatch, employeeEntity);
            var employeeToPatch = _mapper.Map<EmployeeForUpdateDto>(employeeDb);
            return (employeeToPatch: employeeToPatch, employeeEntity: employeeDb);
        }

        public async Task SaveChangesForPatchAsync(EmployeeForUpdateDto employeeToPatch, Employee employeeEntity)
        {
            _mapper.Map(employeeToPatch, employeeEntity);
            await _repository.SaveAsync();
        }

    }
}
