using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AcmeCorpCustomerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using AcmeCorpCustomerAPI.Interfaces;
using AcmeCorpCustomerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

// Uncomment if using a custom DI container
// builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
// builder.Host.ConfigureContainer<ContainerBuilder>(startup.ConfigureContainer);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.MapControllers();
app.Run();