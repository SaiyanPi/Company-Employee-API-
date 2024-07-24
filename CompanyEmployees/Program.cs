using AspNetCoreRateLimit;
using CompanyEmployees;
using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
using CompanyEmployees.Utility;
using Contracts;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;
using NLog;
using Service.DataShaping;
using Shared.DataTransferObjects;

var builder = WebApplication.CreateBuilder(args);

//For configuring Logger Service for Logging Messages
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),"/nlog.config"));

// Added CORS services to the container.
builder.Services.ConfigureCors();
//
// Added iis services to the container.
builder.Services.ConfigureIISIntegration();
//
// Added logger service to the container
builder.Services.ConfigureLoggerService();
//
// Added RepositoryManager service to the container
builder.Services.ConfigureRepositoryManager();
//
// Added ServiceManager service to the container
builder.Services.ConfigureServiceManager();
//
// Added runtime RepositoryContext service to the container
builder.Services.ConfigureSqlContext(builder.Configuration);
//
// Added auotmapper
builder.Services.AddAutoMapper(typeof(Program));
//

// Enable custom respopnses prevented by [ApiController] attribute in controller.
// Adding this code or removing that attribute is same thing
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
//
// Added action filter
builder.Services.AddScoped<ValidationFilterAttribute>();
//
builder.Services.AddScoped<ValidateMediaTypeAttribute>();

// Added DataShaper
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
//
builder.Services.AddScoped<IEmployeeLinks, EmployeeLinks>();
builder.Services.ConfigureVersioning();
builder.Services.ConfigureResponseCaching(); // for adding cache-store
builder.Services.ConfigureHttpCacheHeaders(); // supporting validation
builder.Services.AddMemoryCache();
builder.Services.ConfigureRateLimitingOptions();
builder.Services.AddHttpContextAccessor();



builder.Services.AddControllers(config =>
{
    config.RespectBrowserAcceptHeader = true;
    config.ReturnHttpNotAcceptable = true;
    //config.InputFormatters.Insert(0, MyJPIF.GetJsonPatchInputFormatter());
    config.CacheProfiles.Add("120SecondsDuration", new CacheProfile
    {
        Duration = 120
    });
})
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters()
    .AddCustomCSVFormatter()
    .AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);

builder.Services.AddCustomMediaTypes();


var app = builder.Build();

//added for exception handeling
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsProduction())
    app.UseHsts();
//

app.UseHttpsRedirection();
// Method added
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseIpRateLimiting();
app.UseCors("CorsPolicy");
//
app.UseResponseCaching(); // for adding cache-store
app.UseHttpCacheHeaders(); // supporting validation


app.UseAuthorization();



app.MapControllers();

app.Run();


