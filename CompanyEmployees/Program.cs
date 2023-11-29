using CompanyEmployees.Extensions;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

var app = builder.Build();

if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage(); 
else app.UseHsts();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseStaticFiles(); 
app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.All }); 
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
