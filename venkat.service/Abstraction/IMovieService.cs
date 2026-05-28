using venkat.Common.Models;

namespace venkat.service.Abstraction
{
    public interface IMovieService
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(
            int pageNumber,
            int pageSize);

        Task<Movie> GetMovieByGuidAsync(string guid);

        Task<int> InsertMovieAsync(Movie movie);

        Task<int> UpdateMovieAsync(Movie movie);

        Task<int> DeleteMovieAsync(int id);
    }
}