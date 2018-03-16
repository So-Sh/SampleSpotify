using Newtonsoft.Json;
using SampleSpotify.Models;
using SampleSpotify.Models.Spotify;
using SampleSpotify.Utilities.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleSpotify.Services
{
    public class SpotifyService:ISpotifyService
    {
        private readonly IHttpClient _httpClient;
        private string _token;
        private const string TOKEN_ACCESS_STRING = "grant_type=client_credentials";


        public SpotifyService(IHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void Init()
        {
            _token = GetAccessToken();
        }

        public List<Track> GetRecommendations(List<string> genres)
        {
            string genre = String.Join(",", genres.Take(5));
            var parameters = new Dictionary<string, string>()
            {
                {"market",Configuration.SpotifyMarket},
                {"seed_genres", genre }
            };
            var serverResponse = _httpClient.Get(Configuration.SpotifyRecommendationUrl, parameters, GetAuth());
            var recommendation = JsonConvert.DeserializeObject<RecommendationResponse>(serverResponse);

            recommendation.tracks.ForEach(t =>
            {
                TimeSpan timeSpan = TimeSpan.FromMilliseconds(t.duration_ms);
                t.duration = string.Format("{0:D2}:{1:D2}",
                        timeSpan.Minutes,
                        timeSpan.Seconds
                        );

            });

            return recommendation.tracks;
        }

        public GenreList GetGenres()
        {
            var serverResponse = _httpClient.Get(Configuration.SpotifyGenreUrl, null, GetAuth());
            var jsonGenres =  JsonConvert.DeserializeObject<Genres>(serverResponse);
            var genres = new GenreList() { Genres = new List<Genre>()};

            jsonGenres.genres.ForEach(g => {
                genres.Genres.Add(new Genre()
                {
                    Name = g,
                    IsChecked = false
                });
            });

            return genres;
        }

        private string GetAuth()
        {
            return string.Format("Bearer {0}", _token);
        }

        private string GetAccessToken()
        {
           var serverResponse = _httpClient.Post(Configuration.SpotifyAccessTokenUrl, TOKEN_ACCESS_STRING, Configuration.SpotifyClientId, Configuration.SpotifyClientSecret);
           var token = JsonConvert.DeserializeObject<SpotifyToken>(serverResponse);
           return token.access_token;
        }

 
    }
}