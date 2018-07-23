using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


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

        public async Task<IEnumerable<Recipe>> SearchRecipes(string queryString)
        {
            UriBuilder uri = new UriBuilder("https://api.edamam.com/search");
            uri.Query = $"app_id={appId}&app_key={appKey}&q={queryString}";
            var response = await Client.GetAsync(uri.ToString());

            if (response.IsSuccessStatusCode)
            {
                EdamamResponse deserializedResponse = JsonConvert.DeserializeObject<EdamamResponse>(await response.Content.ReadAsStringAsync());
                IEnumerable<Recipe> recipes = deserializedResponse.hits.Select(x => x.recipe);
                return recipes;
            }
            else
            {
                return null;
            }
        }

    }
}
