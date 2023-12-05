using Entities.Models;

namespace Contracts;
public interface IEmployeeRepository
{
    public IEnumerable<Employee> GetEmployees(Guid companyId, bool trackChanges);
}
