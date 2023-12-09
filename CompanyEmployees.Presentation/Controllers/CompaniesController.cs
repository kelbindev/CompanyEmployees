﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Service.Contracts;

namespace CompanyEmployees.Presentation.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{
    private readonly IServiceManager _service;

    public CompaniesController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetCompanies()
    {
        var companies = _service.CompanyService.GetAllCompanies(trackChanges: false);
        return Ok(companies);
    }
    [HttpGet("{id:guid}", Name ="CompanyById")] 
    public IActionResult GetCompany(Guid id) { 
        var company = _service.CompanyService.GetCompany(id, trackChanges: false); 
        return Ok(company); 
    }
    [HttpPost]
    public IActionResult CreateCompany(CompanyForCreationDto company)
    {
        if (company is null) return BadRequest("Company is null");

        var createdCompany = _service.CompanyService.CreateCompany(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }
    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public IActionResult GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) {
        var companies = _service.CompanyService.GetByIds(ids, trackChanges: false); 
        
        return Ok(companies); 
    }

    [HttpPost("collection")] 
    public IActionResult CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection) 
    {
        var result = _service.CompanyService.CreateCompanyCollection(companyCollection);
        
        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies); 
    }

    [HttpDelete("{id:guid}")] 
    public IActionResult DeleteCompany(Guid id) {
        _service.CompanyService.DeleteCompany(id, trackChanges: false); 
        
        return NoContent(); 
    }
}
