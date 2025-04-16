using DataAccess.Seeders;
using Presentation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ConfigureLogger();
builder.Host.UseNLogConfiguration();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureGeminiSettings(builder.Configuration);
builder.Services.ConfigureRepositories();
builder.Services.ConfigureServices();
builder.Services.ConfigureIdentity();
builder.Services.ConfigureAutoMapper();

var app = builder.Build();

app.ConfigureMiddleware();
app.ConfigureEndpoints();

await AdminSeeder.SeedRolesAndAdminAsync(app.Services);

app.Run();