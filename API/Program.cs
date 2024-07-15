using Domain.Interfaces.Generics;
using Domain.Interfaces.ICategory;
using Domain.Interfaces.IFinanceSystem;
using Domain.Interfaces.ITransaction;
using Domain.Interfaces.IUserFinanceSystem;
using Entities.Entities;
using Infraestructure.Configurations;
using Infraestructure.Repository;
using Infraestructure.Repository.Generics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(
                   builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationContext>();

builder.Services.AddSingleton(typeof(GenericInterface<>), typeof(RepositoryGeneric<>));
builder.Services.AddSingleton<ICategory, RepositoryCategory>();
builder.Services.AddSingleton<ITransaction, RepositoryTransaction>();
builder.Services.AddSingleton<IFinanceSystem, RepositoryFinanceSystem>();
builder.Services.AddSingleton<IUserFinanceSystem, RepositoryUserFinanceSystem>();

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
