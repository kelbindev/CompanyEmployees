using AutoMapper;
using Contracts;
using Service.Contracts;

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
}
