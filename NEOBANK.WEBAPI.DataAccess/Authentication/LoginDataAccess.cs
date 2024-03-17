using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NEOBANK.WEBAPI.Model.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOBANK.WEBAPI.DataAccess.Authentication
{
    public class LoginDataAccess : DataAccessBase
    {
        public LoginDataAccess(TokenModel token) : base(token) 
        {
            
        }

        public async Task<int> LoginVMail (string username, string email)
        {
            try
            {
                using (var context = GetFirstDatabaseConnection())
                {
                    var p_username = new SqlParameter("@username", username);
                    var p_email = new SqlParameter("@email", email);
                    SqlParameter[] parmCollection = { p_username, p_email };
                    var result = await context.Database.ExecuteSqlRawAsync("EXEC neo.NB_API_Login @username @email", parmCollection);
                    return result;
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
