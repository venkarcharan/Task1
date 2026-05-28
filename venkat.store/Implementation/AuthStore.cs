using Dapper;

using Microsoft.Data.SqlClient;

using Microsoft.Extensions.Configuration;

using System.Data;

using venkat.Common.DTOs;

using venkat.store.Abstraction;

namespace venkat.store.Implementation
{
    public class AuthStore : IAuthStore
    {
        private readonly IConfiguration _configuration;

        public AuthStore(
            IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(
                    _configuration.GetConnectionString(
                        "DefaultConnection"));
            }
        }

        public async Task<LoginResponse> LoginAsync(
            LoginRequest request)
        {
            using var connection = Connection;

            return await connection
                .QueryFirstOrDefaultAsync<LoginResponse>(
                    "usp_LoginUser",

                    new
                    {
                        request.UserName
                    },

                    commandType:
                    CommandType.StoredProcedure);
        }
    }
}