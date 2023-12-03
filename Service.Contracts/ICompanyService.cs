using Shared.Dto;

namespace Service.Contracts;
public interface ICompanyService
{
    IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges);
}
