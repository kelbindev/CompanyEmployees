﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Service.Contracts;
using Shared.Dto;

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
    public async Task<IActionResult> GetCompanies()
    {
        var companies = await _service.CompanyService.GetAllCompaniesAsync(trackChanges: false);
        return Ok(companies);
    }
    [HttpGet("{id:guid}", Name ="CompanyById")] 
    public async Task<IActionResult> GetCompany(Guid id) { 
        var company = await _service.CompanyService.GetCompanyAsync(id, trackChanges: false); 
        return Ok(company); 
    }
    [HttpPost]
    public async Task<IActionResult> CreateCompany(CompanyForCreationDto company)
    {
        if (company is null) return BadRequest("Company is null");

        var createdCompany = await _service.CompanyService.CreateCompanyAsync(company);

        return CreatedAtRoute("CompanyById", new { id = createdCompany.Id }, createdCompany);
    }
    [HttpGet("collection/({ids})", Name = "CompanyCollection")]
    public async Task<IActionResult> GetCompanyCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))] IEnumerable<Guid> ids) {
        var companies = await _service.CompanyService.GetByIdsAsync(ids, trackChanges: false); 
        
        return Ok(companies); 
    }

    [HttpPost("collection")] 
    public async Task<IActionResult> CreateCompanyCollection(IEnumerable<CompanyForCreationDto> companyCollection) 
    {
        var result = await _service.CompanyService.CreateCompanyCollectionAsync(companyCollection);
        
        return CreatedAtRoute("CompanyCollection", new { result.ids }, result.companies); 
    }

    [HttpDelete("{id:guid}")] 
    public async Task<IActionResult> DeleteCompany(Guid id) {
        await _service.CompanyService.DeleteCompanyAsync(id, trackChanges: false); 
        
        return NoContent(); 
    }

    [HttpPut("{id:guid}")] 
    public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] CompanyForUpdateDto company) {
        if (company is null) 
            return BadRequest("CompanyForUpdateDto object is null"); 
        
        await _service.CompanyService.UpdateCompanyAsync(id, company, trackChanges: true); 
        
        return NoContent(); 
    }
}
