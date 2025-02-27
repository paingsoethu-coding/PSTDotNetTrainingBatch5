using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSTDotNetTrainingBatch5.ConsoleApp
{
    public class AppSettings
    {
        // Example of Singleton DI`s Connection String
        public static string ConnectionString { get; } = "Data Source= MSI\\SQLEXPRESS2022; Initial Catalog=DotNetTrainingBatch5; User ID=sa; Password=sasa; TrustServerCertificate=True;";
    }
}
