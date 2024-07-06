
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
        ////IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);

        ////getting a single resource(company) from db
        //CompanyDto GetCompany(Guid companyId, bool trackChanges);

        ////for POST method
        //CompanyDto CreateCompany(CompanyForCreationDto company);

        ////for creating a collection of resources
        //IEnumerable<CompanyDto> GetByIds(IEnumerable<Guid> ids, bool trackChanges);
        //(IEnumerable<CompanyDto> companies, string ids) CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection);

        //void DeleteCompany(Guid companyId, bool trackChanges);

        //void UpdateCompany(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);

        Task<IEnumerable<CompanyDto>> GetAllCompaniesAsync(bool trackChanges);
        Task<CompanyDto> GetCompanyAsync(Guid companyId, bool trackChanges);
        Task<CompanyDto> CreateCompanyAsync(CompanyForCreationDto company);
        Task<IEnumerable<CompanyDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
        Task<(IEnumerable<CompanyDto> companies, string ids)> CreateCompanyCollectionAsync (IEnumerable<CompanyForCreationDto> companyCollection);
        Task DeleteCompanyAsync(Guid companyId, bool trackChanges);
        Task UpdateCompanyAsync(Guid companyid, CompanyForUpdateDto companyForUpdate, bool trackChanges);

    }
}
`