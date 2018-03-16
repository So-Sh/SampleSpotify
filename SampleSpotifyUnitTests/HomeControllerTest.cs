using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SampleSpotify.Controllers;
using SampleSpotify.Services;
using System.Web.Mvc;
using SampleSpotify.Models;
using System.Collections.Generic;

namespace SampleSpotifyUnitTests
{
    [TestClass]
    public class HomeControllerTest
    {
        [TestMethod]
        public void TestHomeViewData()
        {
            ISpotifyService mockingService = new SpotifyMockingService();
            var controller = new HomeController(mockingService);
            var result = controller.Index() as ViewResult;
            var genreList = (GenreList) result.ViewData.Model;

            Assert.AreEqual(3, genreList.Genres.Count);
        }

        [TestMethod]

        public void TestTrackViewData()
        {
            ISpotifyService mockingService = new SpotifyMockingService();
            var controller = new HomeController(mockingService);
            var result = controller.Index() as ViewResult;
            var genreList = (GenreList)result.ViewData.Model;
            var genresResult = controller.PostGenres(genreList) as ViewResult;
            var recommendations = (List<Track>) genresResult.ViewData.Model;

            Assert.AreEqual(3, recommendations.Count);
        }
    }
}
