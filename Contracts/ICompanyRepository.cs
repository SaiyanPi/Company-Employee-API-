using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface ICompanyRepository
    {
        //IEnumerable<Company> GetAllCompanies(bool trackChanges);
        Task<IEnumerable<Company>> GetAllCompaniesAsync(bool trackChanges); //making asynchronous

        //getting a single resource from db
        //Company GetCompany(Guid companyId, bool trackChanges);
        Task<Company> GetCompanyAsync(Guid companyId, bool trackChanges);
        Company GetCompany(Guid companyId, bool trackChanges); //for PATCH 

        //for POST method
        void CreateCompany(Company company);

        //for creating a collection of resources
        //IEnumerable<Company> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        Task<IEnumerable<Company>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);

        void DeleteCompany(Company company);

        //Create and Delete method signatures are left synchronous.
        //That's because, in these methods, we are not making any changes in the database.
        //All we are doing is changing the state of the entity to added and deleted
    }
}
