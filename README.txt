This is a simple website that allows a user to apply for a credit card, and displays a decision about which card they are eligible for, if any.

The easiest way to build and run it is to use Visual Studio. Copy this entire folder to a folder on your local machine, then open the solution
file (.sln) in Visual Studio and run it (Debug -> Start Without Debugging, or Ctrl-F5). If you don't have the System.Configuration.ConfigurationManager and System.Data.SqlClient packages, you will need to install them via nuget.

Credit card applications can be recorded in a database or a comma-separated text file. This is configurable via the RequestLogging section of the appsettings.json file. For example:

  "RequestLogging": {
    "ConnectionString": "Server=localhost\\SQLEXPRESS;Database=master;Trusted_Connection=True;",
    "RequestFilePath": "requestlog.txt",
    "ErrorFilePath": "errorlog.txt"
  }

If the "ConnectionString" setting is supplied, credit card applications will be recorded in the specified SQL database, using the table dbo.requests. This has only been tested using SQL Server, but should work with any SQL database.

If the "ConnectionString" setting is not supplied, credit card applications will be recorded in a text file. Use the "RequestFilePath" setting to specify a full or relative path to the file (the directory must already exist). Use "\\" as a directory separator, for example "C:\\MyFolder\\log.txt". If "RequestFilePath" is not specified, it defaults to log.txt in the top-level CardPreQualificationTool folder.

If an error occurs, such as being unable to write to the database or log file, the error is logged to the file specified in "ErrorFilePath", or to error.txt if this setting is not provided.