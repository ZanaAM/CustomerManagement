using CustomerManagement.API.Core.Configurations;
using CustomerManagement.API.Data;
using CustomerManagement.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;
using FluentValidation.AspNetCore;
using System.Configuration;
using CustomerManagement.API.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Configuration.GetConnectionString("CustomerManagementDbConnectionString");
builder.Services.AddDbContext<CustomerManagementDbContext>(options => {
    options.UseSqlServer(connectionString);
});
builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<CustomerValidator>());
builder.Services.AddFluentValidation(fv =>
    fv.RegisterValidatorsFromAssemblyContaining<AddressValidator>());
builder.Services.AddAutoMapper(typeof(MapperConfig));
builder.Services.AddApplicationServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options => {
    options.AddPolicy("AllowAll",
        b => b.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod());
});

builder.Host.UseSerilog((context, loggerConfig) => 
                          loggerConfig.WriteTo.Console()
                             .ReadFrom.Configuration(context.Configuration));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
