using Contracts;
using Service.Contracts;
using Shared.Dto;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;

    public CompanyService(IRepositoryManager repositoryManager, ILoggerManager logger)
    {
        _repositoryManager = repositoryManager;
        _logger = logger ;
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        try
        {
            var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);

            var companiesDto = companies.Select(c => 
                new CompanyDto(c.Id, c.Name ?? "", string.Join(' ', c.Address, c.Country)))
                .ToList(); 

            return companiesDto;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong in the {nameof(GetAllCompanies)} service method {ex}");
            throw;
        }
    }
}
