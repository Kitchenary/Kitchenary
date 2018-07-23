using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace EdamamService
{
    public class EdamamClient
    {
        private string appId;
        private string appKey;
        private HttpClient Client {get; set;}

        public EdamamClient(string appId, string appKey)
        {
            Client = new HttpClient();
            this.appId = appId;
            this.appKey = appKey;
        }

        public async Task<string> SearchRecipes(string queryString)
        {
            UriBuilder uri = new UriBuilder("https://api.edamam.com/search");
            uri.Query = $"app_id={appId}&app_key={appKey}&q={queryString}";
            var response = await Client.GetAsync(uri.ToString());

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return null;
            }
        }

    }
}
