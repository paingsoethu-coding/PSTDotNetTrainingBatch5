// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PSTDotNetTrainingBatch5.ConsoleApp4;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");


#region AdoDotNet

// New way to get connection string from appsettings.json (should approach for dynamic)
//var configuration = new ConfigurationBuilder()
//    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//    .Build();

//var serviceProvider = new ServiceCollection()
//    .AddSingleton<IConfiguration>(configuration)
//    .AddSingleton<AppSettings>()
//    .AddSingleton<IDbConnection>(sp => new SqlConnection(sp.GetRequiredService<AppSettings>().GetConnectionString()))
//    .BuildServiceProvider();

//var dbConnection = serviceProvider.GetRequiredService<IDbConnection>();
//Console.WriteLine("Database connected successfully!");

var connectionString = AppSettings.ConnectionString;

var adoDotNetServices = new ServiceCollection()
    // Dependency Injection declaration parts
    .AddSingleton<SqlConnection>(sp => new SqlConnection(connectionString))
    .AddSingleton<IAdoDotNetExample ,AdoDotNetExample>()
    .BuildServiceProvider();

// Sql Connection can only use once. if you want to use it all, method injection is better.
var adoDotNetExample = adoDotNetServices.GetRequiredService<IAdoDotNetExample>();
adoDotNetExample.Read();
//adoDotNetExample.Create();
//adoDotNetExample.Edit();
//adoDotNetExample.Update();
//adoDotNetExample.Delete();

#endregion


#region Dapper

//var connectionString = AppSettings.ConnectionString;

//    // To use EFCore, we need to add the following AddDbContext
//    // DI lifetime scope
//    // BuildServiceProvider

//var dapperServices = new ServiceCollection()
//    .AddSingleton<IDbConnection>(sp => new SqlConnection(connectionString))
//    .AddSingleton<IDapperExample, DapperExample>()
//    .BuildServiceProvider();

//var dapperExample = dapperServices.GetRequiredService<IDapperExample>();
//dapperExample.Read();
//dapperExample.Create("TitleDI", "AuthorDI", "ContentDI");
//dapperExample.Edit(2);
//dapperExample.Update();
//dapperExample.Delete();

#endregion


#region EFcore
//var eFcoreServices = new ServiceCollection()
//    // To use EFCore, we need to add the following AddDbContext
//    // DI lifetime scope
//    // BuildServiceProvider
//    .AddDbContext<AppDbContext>(options => 
//    options.UseSqlServer("Data Source= MSI\\SQLEXPRESS2022; " +
//    "Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;"))
//    .AddSingleton<IEFCoreExample ,EFCoreExample>()
//    .BuildServiceProvider();

//var eFcoreExample = eFcoreServices.GetRequiredService<IEFCoreExample>();

//eFcoreExample.Read();
//eFcoreExample.Create("TitleDI", "AuthorDI", "ContentDI");
//eFcoreExample.Edit(1);
//eFcoreExample.Update(1, "TitleDI-1", "AuthorDI-1", "ContentDI-1");
//eFcoreExample.Delete(1);
#endregion


Console.ReadKey();

