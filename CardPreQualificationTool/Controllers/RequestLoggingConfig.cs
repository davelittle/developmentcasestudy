namespace CardPreQualificationTool.Controllers
{
    // Represents the RequestLogging section of the appsettings.json file
    public class RequestLoggingConfig
    {
        // SQL Server database connection string for logging requests (if null, requests are logged to a file instead)
        public string ConnectionString { get; set; }

        // Path of log file for logging requests if a database is not being used
        public string RequestFilePath { get; set; }

        // Log file for logging any exceptions that occur (e.g. failure to log a request)
        public string ErrorFilePath { get; set; }
    }
}
