using CompanyEmployees;
using CompanyEmployees.Extensions;
using CompanyEmployees.Presentation.ActionFilters;
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
// Added action filter
builder.Services.AddScoped<ValidationFilterAttribute>();
//
// Added DataShaper
builder.Services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
//

// Enable custom respopnses prevented by [ApiController] attribute in controller.
// Adding this code or removing that attribute is same thing
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
//

builder.Services.AddControllers(config => {
    config.RespectBrowserAcceptHeader = true; // Added for formatting response

    // if the client tries to negotiate for the media type the server doesn’t support,
    // it should return the 406 Not Acceptable statuscode.
    config.ReturnHttpNotAcceptable = true;
    //for PATCH
    //config.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
})
.AddNewtonsoftJson() //for PATCH

// Added for formatting response
.AddXmlDataContractSerializerFormatters()

// Added for custom formatter
.AddCustomCSVFormatter()

// Adding Controller service from the Presentation Project
.AddApplicationPart(typeof(CompanyEmployees.Presentation.AssemblyReference).Assembly);


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
app.UseCors("CorsPolicy");
//
app.UseAuthorization();



app.MapControllers();

app.Run();
//NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
//{
//    return new ServiceCollection().AddLogging().AddMvc().AddNewtonsoftJson()
//    .Services.BuildServiceProvider()
//    .GetRequiredService<IOptions<MvcOptions>>().Value.InputFormatters
//    .OfType<NewtonsoftJsonPatchInputFormatter>().First();
//}