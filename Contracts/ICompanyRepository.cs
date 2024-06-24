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
        IEnumerable<Company> GetAllCompanies(bool trackChanges);

        //getting a single resource from db
        Company GetCompany(Guid companyId, bool trackChanges);

        //for POST method
        void CreateCompany(Company company);

    }
}
