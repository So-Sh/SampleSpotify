using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SampleSpotify.Utilities.Http
{
    public class HttpClient : IHttpClient
    {
        public string Get(string url)
        {
            return Get(url, null,"");
        }

        public string Get(string url, IDictionary<string, string> parameters)
        {
            return Get(url, parameters, "");
        }


        public string Get(string url, IDictionary<string, string> parameters, string authorization)
        {
            var request = (HttpWebRequest)WebRequest.Create(url + GenerateParameterUrlString(parameters));

            request.Method = "GET";
            request.ContentLength = 0;
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(authorization))
            {
                request.Headers.Add("Authorization", authorization);
            }
            try
            {
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ApplicationException(string.Format("Request failed. Received HTTP {0}", response.StatusCode));
                    }

                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                        {
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                        }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public string Post(string url, string postString)
        {
            return Post(url,postString,"","");
        }


        public string Post(string url, string postString, string clientId, string clientSecret )
        {

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);

            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.Accept = "application/json";
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientId))
	        {
                var encodedCredentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", clientId, clientSecret)));
                webRequest.Headers.Add("Authorization: Basic " + encodedCredentials);

	        }

            var request = (postString);
            byte[] req_bytes = Encoding.ASCII.GetBytes(postString);
            webRequest.ContentLength = req_bytes.Length;

            Stream strm = webRequest.GetRequestStream();
            strm.Write(req_bytes, 0, req_bytes.Length);
            strm.Close();

            HttpWebResponse resp = (HttpWebResponse)webRequest.GetResponse();
            String responseFromServer = "";
            using (Stream respStr = resp.GetResponseStream())
            {
                using (StreamReader rdr = new StreamReader(respStr, Encoding.UTF8))
                {
                    //should get back a string i can then turn to json and parse for accesstoken
                    responseFromServer = rdr.ReadToEnd();
                    rdr.Close();
                }
            }
            return responseFromServer;
        }

        private string GenerateParameterUrlString(IDictionary<string, string> parameters)
        {
            if (parameters == null)
            {
                return null;
            }

            string value;
            var parametersStringBuilder = new StringBuilder();

            foreach (var key in parameters.Keys)
            {
                if (!parameters.TryGetValue(key, out value))
                {
                    throw new Exception(string.Format("No value is found for key {0}", key));
                }

                if (parametersStringBuilder.Length > 0)
                {
                    parametersStringBuilder.Append(string.Format("&{0}={1}", key,value));
                }
                else
                {
                    parametersStringBuilder.Append(string.Format("?{0}={1}", key, value));
                }
            }

            return parametersStringBuilder.ToString();
        }
    }
}