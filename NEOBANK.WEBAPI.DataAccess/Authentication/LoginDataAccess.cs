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
                    SqlParameter[] parmCollection = { p_username, p_email, p_password, p_isGoogle, p_value };
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

        public async Task<string> GetSaltbyEmail (string email)
        {
            try
            {
                using (var context = GetFirstDatabaseConnection())
                {
                    var p_email = new SqlParameter("@email", email);
                    var p_code = new SqlParameter
                    {
                        ParameterName = "@code",
                        SqlDbType = System.Data.SqlDbType.NVarChar,
                        Size = 1000,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    SqlParameter[] parmCollection = { p_email, p_code };
                    await context.Database.ExecuteSqlRawAsync("EXEC neo.NB_API_GetSaltbyEmail @email, @code output", parmCollection);
                    return p_code.Value.ToString();
                }
            }
            catch (Exception ex) 
            { 
                throw ex; 
            }
        }

        public async Task<int> CreateUser(string username, string email, string password, string emailHash, string firstName, string lastName)
        {
            try
            {
                using (var context = GetFirstDatabaseConnection())
                {
                    var p_username = new SqlParameter("@username", username);
                    var p_email = new SqlParameter("@email", email);
                    var p_password = new SqlParameter("@password", password);
                    var p_emailHash = new SqlParameter("@emailHash", emailHash);
                    var p_firstName = new SqlParameter("@firstName", firstName);
                    var p_lastName = new SqlParameter("@lastName", lastName);
                    var p_value = new SqlParameter
                    {
                        ParameterName = "@value",
                        SqlDbType = System.Data.SqlDbType.Int,
                        Direction = System.Data.ParameterDirection.Output
                    };
                    SqlParameter[] parmCollection = { p_username, p_email, p_password, p_emailHash, p_firstName, p_lastName, p_value };
                    var result = await context.Database.ExecuteSqlRawAsync("EXEC neo.NB_API_CreateUser @username, @email, @password, @emailHash, @firstName, @lastName, @value output", parmCollection);

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
