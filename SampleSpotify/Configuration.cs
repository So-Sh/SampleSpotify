using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SampleSpotify
{
    public static class Configuration
    {
        private static AppSettingsReader _appSettingsReader = new AppSettingsReader();

        public static string SpotifyAccessTokenUrl => _appSettingsReader.GetValue("SpotifyAccessTokenUrl", typeof(string)).ToString();
        public static string SpotifyClientId => _appSettingsReader.GetValue("SpotifyClientId", typeof(string)).ToString();
        public static string SpotifyClientSecret => _appSettingsReader.GetValue("SpotifyClientSecret", typeof(string)).ToString();
        public static string SpotifyMarket => _appSettingsReader.GetValue("SpotifyMarket", typeof(string)).ToString();
        public static string SpotifyRecommendationUrl => _appSettingsReader.GetValue("SpotifyRecommendationUrl", typeof(string)).ToString();
        public static string SpotifyGenreUrl => _appSettingsReader.GetValue("SpotifyGenreUrl", typeof(string)).ToString();

    }
}