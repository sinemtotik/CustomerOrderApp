using Autofac.Extensions.DependencyInjection;
using Autofac;
using CustomerOrderApp.API.Modules;
using CustomerOrderApp.Core;
using CustomerOrderApp.Core.Repositories;
using CustomerOrderApp.Core.Services;
using CustomerOrderApp.Repository;
using CustomerOrderApp.Repository.Repositories;
using CustomerOrderApp.Repository.UnitOfWorks;
using CustomerOrderApp.Service.Mapping;
using CustomerOrderApp.Service.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));


builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
});


builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());


builder.Host.ConfigureContainer<ContainerBuilder>
    (containerBuilder => containerBuilder.RegisterModule(new RepositoryServiceModule()));


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
