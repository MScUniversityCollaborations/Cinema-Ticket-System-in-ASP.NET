using MovieTickets.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace MovieTickets.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/movies
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.ToList();
        }

        // GET /api/movies/1
        public Movie GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);    
            
            return movie;
        }

        // POST /api/movies
        [HttpPost]
        public Movie CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            _context.Movies.Add(movie);
            _context.SaveChanges();

            return movie;
        }

        // POST /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            movieInDb.Title = movie.Title;
            movieInDb.Cast = movie.Cast;
            movieInDb.Director = movie.Director;
            movieInDb.Description = movie.Description;
            movieInDb.DurationMin = movie.DurationMin;
            movieInDb.ImagePoster = movie.ImagePoster;

            _context.SaveChanges();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
