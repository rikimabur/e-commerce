using BuildingBlocks.API.Modules;
using BuildingBlocks.Application;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options => options.AddDefaultPolicy(
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod()));

// Common services

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "ModularPlatform API", Version = "v1" }));

//builder.Services.AddMediatR(cfg =>
//    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

builder.Services.AddAuthentication();
builder.Services.AddApplication(typeof(Program).Assembly)
                .AddInfrastructure()
                .AddEndpoints(typeof(Program).Assembly);
// Discover modules
var modules = ModuleDiscovery.DiscoverModules();
foreach (var module in modules)
{
    module.Register(builder.Services, builder.Configuration);
}

var app = builder.Build();
app.UseCors();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwagger();

    // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
    app.UseExceptionHandler("/error-development");

    app.MapOpenApi();
}
app.UseAuthentication();
app.MapEndpoints();

app.UseHttpsRedirection();


app.Run();
