using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.DataAccess
{
    public class DataAccessBase
    {
        public string? userId { get; set; }

        private TokenModel? token = null;

        public DataAccessBase(TokenModel _token)
        {
            this.token = _token;
            userId = _token.userId;
        }

        public DatabaseContext GetDatabaseConnection()
        {
            return new DatabaseContext(token.DatabaseConnection);
        }
        public DatabaseContext GetDatabaseConnection(string connectionString)
        {
            return new DatabaseContext(connectionString);
        }
        public DatabaseContext GetFirstDatabaseConnection()
        {
            string databaseServerName = ConfigurationManager.AppSettings["DatabaseServerName"];
            string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
            string databaseUsername = ConfigurationManager.AppSettings["DatabaseUsername"];
            string databasePassword = ConfigurationManager.AppSettings["DatabasePassword"];

            string dbConnection = @"Server=" + databaseServerName
                                    + ";Database=" + databaseName
                                    + ";User Id=" + databaseUsername
                                    + ";Password=" + databasePassword + ";";
            return new DatabaseContext(dbConnection);
        }

    }
}
