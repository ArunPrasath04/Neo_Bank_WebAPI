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

        public async Task<int> LoginVMail (string username, string email, string password, bool isGoogle)
        {
            try
            {
                using (var context = GetFirstDatabaseConnection())
                {
                    var p_username = new SqlParameter("@username", username);
                    var p_email = new SqlParameter("@email", email);
                    var p_password = password != null ? new SqlParameter("@password", password) : new SqlParameter("@password", System.DBNull.Value);
                    var p_isGoogle = new SqlParameter("@isGoogle", isGoogle);
                    var p_value = new SqlParameter
                    {
                        ParameterName = "@value",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    SqlParameter[] parmCollection = { p_username, p_email, p_password, p_isGoogle };
                    var result = await context.Database.ExecuteSqlRawAsync("EXEC neo.NB_API_Login @username, @email, @password, @isGoogle, @value output", parmCollection);

                    int val = Convert.ToInt32(p_value.Value.ToString());
                    return val;
                }
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
