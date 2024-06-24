
using Shared.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface ICompanyService
    {
        IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);

        //getting a single resource(company) from db
        CompanyDto GetCompany(Guid companyId, bool trackChanges);

        //for POST method
        CompanyDto CreateCompany(CompanyForCreationDto company);


    }
}
