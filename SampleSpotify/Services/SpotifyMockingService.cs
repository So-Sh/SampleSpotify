using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SampleSpotify.Models;

namespace SampleSpotify.Services
{
    public class SpotifyMockingService : ISpotifyService
    {
        public GenreList GetGenres()
        {
            return new GenreList()
            {
                Genres = new List<Genre>()
                {
                    new Genre() { Name = "Pop"},
                    new Genre() { Name = "Rock"},
                    new Genre() { Name = "Jazz"},
                }
            };
        }

        public List<Track> GetRecommendations(List<string> genres)
        {
            return new List<Track>() {
                new Track() {
                    name = "Castle Of Glass",
                    duration = "03:45",
                    artists = new List<Models.Spotify.Artist>()
                    {
                        new Models.Spotify.Artist()
                        {
                            name = "Linkin Park"
                        }
                    }
                },
                                new Track() {
                    name = "School's Out",
                    duration = "03:30",
                    artists = new List<Models.Spotify.Artist>()
                    {
                        new Models.Spotify.Artist()
                        {
                            name = "Alice Cooper"
                        }
                    }
                },
                new Track() {
                    name = "Zombie",
                    duration = "05:06",
                    artists = new List<Models.Spotify.Artist>()
                    {
                        new Models.Spotify.Artist()
                        {
                            name = "The Cranberries"
                        }
                    }
                },

            };
        }

        public void Init()
        {
            return;
        }
    }
}