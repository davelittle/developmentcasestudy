using CardPreQualificationTool.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace CardPreQualificationTool.Controllers
{
    public class DatabaseCardApplicationLogger : ICardApplicationLogger
    {
        private readonly string _connectionString;
        private bool _tableCreated = false;

        public DatabaseCardApplicationLogger(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Log(LogEntry logEntry)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            if (!_tableCreated)
            {
                // Create the table if it doesn't already exist
                string tableCreationSql = "IF OBJECT_ID(N'dbo.requests', N'U') IS NULL BEGIN CREATE TABLE dbo.requests(timestamp DATETIME, firstName NVARCHAR(50), lastName NVARCHAR(50), dateOfBirth DATE, annualIncome DECIMAL(10,2), cardType NVARCHAR(50), interestRate DECIMAL(10,1), rejectionReason NVARCHAR(50)); END";

                using SqlCommand tableCreationCommand = new SqlCommand(tableCreationSql, connection);
                tableCreationCommand.ExecuteNonQuery();
                _tableCreated = true;
            }

            // Add a row to the table giving details of the user's application for a credit card
            string insertSql = "INSERT INTO requests VALUES(@param1,@param2,@param3,@param4,@param5,@param6,@param7,@param8)";
            using SqlCommand cmd = new SqlCommand(insertSql, connection);
            cmd.Parameters.Add("@param1", SqlDbType.DateTime).Value = logEntry.Timestamp;
            cmd.Parameters.Add("@param2", SqlDbType.NVarChar, 50).Value = logEntry.FirstName;
            cmd.Parameters.Add("@param3", SqlDbType.NVarChar, 50).Value = logEntry.LastName;
            cmd.Parameters.Add("@param4", SqlDbType.Date).Value = logEntry.DateOfBirth;
            cmd.Parameters.Add("@param5", SqlDbType.Decimal).Value = logEntry.AnnualIncome;
            AddStringOrNull(cmd.Parameters, "@param6", logEntry.CardType);

            if (logEntry.InterestRate.HasValue)
            {
                cmd.Parameters.Add("@param7", SqlDbType.Decimal).Value = logEntry.InterestRate;
            }
            else
            {
                cmd.Parameters.Add("@param7", SqlDbType.Decimal).Value = DBNull.Value;
            }

            AddStringOrNull(cmd.Parameters, "@param8", logEntry.RejectionReason);            
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }

        private static void AddStringOrNull(SqlParameterCollection parameters, string parameterName, string parameterValue)
        {
            if (parameterValue != null)
            {
                parameters.Add(parameterName, SqlDbType.NVarChar, 50).Value = parameterValue;
            }
            else
            {
                parameters.Add(parameterName, SqlDbType.NVarChar, 50).Value = DBNull.Value;
            }
        }
    }
}
