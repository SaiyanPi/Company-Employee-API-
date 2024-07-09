
using Shared.RequestFeatures;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IEmployeeRepository
    {
        //Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, bool trackChanges);
        //Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, 
        //    EmployeeParameter employeeParameters, bool trackChanges); //added for paging
        Task<PagedList<Employee>> GetEmployeesAsync(Guid companyId,
            EmployeeParameter employeeParameters, bool trackChanges); //final modification for improving paging

        //getting a single resource(EMPLOYEE) from db
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid id, bool trackChanges);

        void CreateEmployeeForCompany(Guid companyId, Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
