using Microsoft.Extensions.Configuration;

namespace PSTDotNetTrainingBatch5.ConsoleApp4;

public class AppSettings
{
    private readonly IConfiguration _configuration;
    public AppSettings(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public static string ConnectionString { get; } = "Data Source= MSI\\SQLEXPRESS2022; " +
        "Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;";

    public string GetConnectionString()
    {
        return _configuration.GetConnectionString("DefaultConnection") ?? throw new Exception("Connection string not found.");
    }
}

