using Clippy_mobile.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Clippy_mobile
{
    class RestClient
    {
        private const string RestServiceBaseAddress = "http://clippy.elasticbeanstalk.com/",
                             AcceptHeaderApplicationJson = "application/json";

        private HttpClient CreateRestClient()
        {
            var client = new HttpClient()
            {
                BaseAddress = new Uri(RestServiceBaseAddress)
            };
            client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(AcceptHeaderApplicationJson));
            return client;
        }

        public async Task<Note> GetNote(string key)
        {
            using (var client = CreateRestClient())
            {
                var getDataResponse = await client.GetAsync(key, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);

                return new Note() { Key = await getDataResponse.Content.ReadAsStringAsync().ConfigureAwait(false) };
            }
        }
    }
}
