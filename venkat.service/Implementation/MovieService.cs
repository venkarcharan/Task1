using venkat.Common.Models;

using venkat.service.Abstraction;

using venkat.store.Abstraction;

namespace venkat.service.Implementation
{
    public class MovieService : IMovieService
    {
        private readonly IMovieStore _movieStore;

        public MovieService(
            IMovieStore movieStore)
        {
            _movieStore = movieStore;
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(
            int pageNumber,
            int pageSize)
        {
            return await _movieStore.GetMoviesAsync(
                pageNumber,
                pageSize);
        }

        public async Task<Movie> GetMovieByGuidAsync(
            string guid)
        {
            return await _movieStore
                .GetMovieByGuidAsync(guid);
        }

        public async Task<int> InsertMovieAsync(
            Movie movie)
        {
            return await _movieStore
                .InsertMovieAsync(movie);
        }

        public async Task<int> UpdateMovieAsync(
            Movie movie)
        {
            return await _movieStore
                .UpdateMovieAsync(movie);
        }

        public async Task<int> DeleteMovieAsync(
            int id)
        {
            return await _movieStore
                .DeleteMovieAsync(id);
        }
    }
}