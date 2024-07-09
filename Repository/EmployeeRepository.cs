using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
        {
        }
        //public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges) => 
        //    await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        //    .OrderBy(e => e.Name)
        //    .ToListAsync();

        //public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId,
        // EmployeeParameter employeeParameters, bool trackChanges) =>
        // await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
        // .OrderBy(e => e.Name)
        // .Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
        // .Take(employeeParameters.PageSize)
        // .ToListAsync();

        public async Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId, //method after final improvement in paging
            EmployeeParameter employeeParameters, bool trackChanges)
        {
            var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            .OrderBy(e => e.Name)
            .ToListAsync();
            return PagedList<Employee>.ToPagedList(employees, employeeParameters.PageNumber, employeeParameters.PageSize);

            // final improvement in paging for very very large amount of data. Since we only have few data,
            // above code is fine but if the data were to be in a millions then switching to following codes would be a much faster
            // START
            //var employees = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges)
            //.OrderBy(e => e.Name)
            //.Skip((employeeParameters.PageNumber - 1) * employeeParameters.PageSize)
            //.Take(employeeParameters.PageSize)
            //.ToListAsync();
            //var count = await FindByCondition(e => e.CompanyId.Equals(companyId), trackChanges
            //).CountAsync();
            //return new PagedList<Employee>(employees, count,
            //employeeParameters.PageNumber, employeeParameters.PageSize);
            // END
        } 

        //getting a single resource(EMPLOYEE) from db
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges) =>
            await FindByCondition(e => e.CompanyId.Equals(companyId) && e.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateEmployeeForCompany(Guid companyId, Employee employee)
        {
            employee.CompanyId = companyId;
            Create(employee);
        }

        public void DeleteEmployee(Employee employee) => Delete(employee);
    }
}

