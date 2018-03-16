using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleSpotify.Utilities.Http
{
    public interface IHttpClient
    {
        string Get(string url);
        string Get(string url, IDictionary<string, string> parameters);
        string Get(string url, IDictionary<string, string> parameters, string authorization);
        string Post(string url, string postString);
        string Post(string url, string postString, string clientId, string clientSecret);

    }
}
