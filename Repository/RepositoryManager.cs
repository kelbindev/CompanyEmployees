using Contracts;

namespace Repository;
public sealed class RepositoryManager : IRepositoryManager
{
    private readonly RepositoryContext _context;
    private readonly Lazy<ICompanyRepository> _company;
    private readonly Lazy<IEmployeeRepository> _employee;

    public RepositoryManager(RepositoryContext context)
    {
        _context = context;
        _company = new Lazy<ICompanyRepository>(() => new CompanyRepository(context));
        _employee = new Lazy<IEmployeeRepository>(() => new EmployeeRepository(context));  
    }

    public ICompanyRepository Company => _company.Value; 
    public IEmployeeRepository Employee => _employee.Value; 
    public async Task SaveAsync() => await _context.SaveChangesAsync();
}
