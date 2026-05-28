using Dapper;

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

using System.Data;

using venkat.Common.Models;
using venkat.Common.Utilities;

using venkat.store.Abstraction;

namespace venkat.store.Implementation
{
    public class MovieStore : IMovieStore
    {
        private readonly IConfiguration _configuration;

        public MovieStore(
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

        // GET MOVIES WITH PAGINATION

        public async Task<IEnumerable<Movie>> GetMoviesAsync(
            int pageNumber,
            int pageSize)
        {
            using var connection = Connection;

            return await connection.QueryAsync<Movie>(
                SqlConstants.GetMovies,

                new
                {
                    PageNumber = pageNumber,
                    PageSize = pageSize
                },

                commandType:
                CommandType.StoredProcedure);
        }


        // GET MOVIE BY GUID

        public async Task<Movie> GetMovieByGuidAsync(
            string guid)
        {
            using var connection = Connection;

            return await connection
                .QueryFirstOrDefaultAsync<Movie>(
                    SqlConstants.GetMovieByGuid,

                    new
                    {
                        MovieGuid = guid
                    },

                    commandType:
                    CommandType.StoredProcedure);
        }


        // INSERT MOVIE

        public async Task<int> InsertMovieAsync(
            Movie movie)
        {
            using var connection = Connection;

            return await connection.ExecuteAsync(
                SqlConstants.InsertMovie,

                new
                {
                    movie.MovieName,
                    movie.Genre,
                    movie.LanguageName,
                    movie.TicketPrice
                },

                commandType:
                CommandType.StoredProcedure);
        }


        // UPDATE MOVIE

        public async Task<int> UpdateMovieAsync(
            Movie movie)
        {
            using var connection = Connection;

            return await connection.ExecuteAsync(
                SqlConstants.UpdateMovie,

                new
                {
                    movie.MovieId,
                    movie.MovieName,
                    movie.Genre,
                    movie.LanguageName,
                    movie.TicketPrice
                },

                commandType:
                CommandType.StoredProcedure);
        }


        // DELETE MOVIE

        public async Task<int> DeleteMovieAsync(
            int id)
        {
            using var connection = Connection;

            return await connection.ExecuteAsync(
                SqlConstants.DeleteMovie,

                new
                {
                    MovieId = id
                },

                commandType:
                CommandType.StoredProcedure);
        }
    }
}