using AutoMapper;
using Contracts;
using Entities.ErrorModel;
using Service.Contracts;
using Shared.Dto;

namespace Service;
internal sealed class EmployeeService : IEmployeeService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _loggerManager;
    private readonly IMapper _mapper;

    public EmployeeService(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IMapper mapper)
    {
        _repositoryManager= repositoryManager;
        _loggerManager= loggerManager;
        _mapper= mapper;
    }

    public IEnumerable<EmployeeDto> GetEmployees(Guid companyId, bool trackChanges)
    {
        var company = _repositoryManager.Company.GetCompany(companyId, trackChanges);

        if (company is null) throw new CompanyNotFoundException(companyId);

        var employeesFromDb = _repositoryManager.Employee.GetEmployees(companyId, trackChanges);

        var employeesDto = _mapper.Map<IEnumerable<EmployeeDto>>(employeesFromDb);

        return employeesDto;
    }

    public EmployeeDto GetEmployee(Guid companyId, Guid id, bool trackChanges)
    {
        var company = _repositoryManager.Company.GetCompany(companyId, trackChanges);

        if (company is null) throw new CompanyNotFoundException(companyId);

        var employeeFromDb = _repositoryManager.Employee.GetEmployee(companyId, id, trackChanges);

        var employeeDto = _mapper.Map<EmployeeDto>(employeeFromDb);

        return employeeDto;
    }

}
