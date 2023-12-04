﻿using AutoMapper;
using Contracts;
using Entities.ErrorModel;
using Service.Contracts;
using Shared.Dto;

namespace Service;
internal sealed class CompanyService : ICompanyService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public CompanyService(IRepositoryManager repositoryManager, ILoggerManager logger, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _logger = logger;
        _mapper = mapper;
    }

    public IEnumerable<CompanyDto> GetAllCompanies(bool trackChanges)
    {
        var companies = _repositoryManager.Company.GetAllCompanies(trackChanges);
        var companiesDto = _mapper.Map<IEnumerable<CompanyDto>>(companies);
        return companiesDto;
    }

    public CompanyDto GetCompany(Guid id, bool trackChanges)
    {
        var company = _repositoryManager.Company.GetCompany(id, trackChanges);

        if (company is null) throw new CompanyNotFoundException(id);
        
        var companyDto = _mapper.Map<CompanyDto>(company); 
        
        return companyDto; }
    }
