using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleSpotify.Models.Spotify
{
    public class RecommendationResponse
    {
        public List<Track> tracks { get; set; }
        public List<Seed> seeds { get; set; }
    }
}