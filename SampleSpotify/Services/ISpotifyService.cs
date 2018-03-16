using SampleSpotify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleSpotify.Services
{
    public interface ISpotifyService
    {
        void Init();
        List<Track> GetRecommendations(List<string> genres);
        GenreList GetGenres();
    }
}
