using Farmasi_Project_API.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

#region Service Register
// Add services to the container.
services.ConfigureServices(builder.Host, builder.Configuration);

#endregion

#region App Builder
var app = builder.Build();
app.Configure();

#endregion