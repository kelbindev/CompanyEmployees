using Shared.Dto;

namespace Service.Contracts;
public interface IEmployeeService
{
    IEnumerable<EmployeeDto> GetEmployees(Guid Id, bool trackChanges);
}
