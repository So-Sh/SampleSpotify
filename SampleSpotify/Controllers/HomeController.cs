using SampleSpotify.Models;
using SampleSpotify.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SampleSpotify.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISpotifyService _spotify;

        public HomeController(ISpotifyService spotify)
        {
            _spotify = spotify;
            _spotify.Init();
        }

        public ActionResult Index()
        {
            var genre = _spotify.GetGenres();
            return View(genre);
        }

        [HttpPost]
        public ActionResult PostGenres(GenreList genreList)
        {
            if (genreList.Genres.Count > 0)
            {
                var selectedGenres = genreList.Genres
                .Where(g => g.IsChecked)
                .Select(g => g.Name)
                .ToList<string>();

                ViewBag.GenresSelected = true;
                var model = _spotify.GetRecommendations(selectedGenres);
                return View("Track", model);
            }
            return new EmptyResult();

        }


    }
}