using Microsoft.EntityFrameworkCore;
using PSTDotNetTrainingBatch5.Database.Models;
using PSTDotNetTrainingBatch5.Domain.Features.Blog;
using PSTDotNetTrainingBatch5.RestApi4.Controllers;
using Microsoft.Extensions.DependencyInjection;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(connectionString);
}, ServiceLifetime.Transient, ServiceLifetime.Transient);

builder.Services.AddScoped<IDbConnection>( sp => 
    new SqlConnection(connectionString));

builder.Services.AddScoped<IBlogServiceV2, BlogV2Service>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
