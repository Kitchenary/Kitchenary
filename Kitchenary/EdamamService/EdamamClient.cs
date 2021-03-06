﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;


namespace EdamamService
{
    public enum Diet
    {
        BALANCED,
        HIGH_PROTEIN,
        LOW_FAT,
        LOW_CARB,
    }

    public enum Health
    {
        DEFAULT,
        ALCOHOL_FREE,
        SUGAR_CONSCIOUS,
        TREE_NUT_FREE,
        VEGAN,
        VEGETARIAN,

    }

    public class RecipeSearchSettings
    {
        public int Time { get; set; }
        public List<string> Excluded { get; set; }
        public Diet Diet { get; set; }
        public List<Health> Health { get; set; }
        public string DietString { get { return Enum.GetName(typeof(Diet), Diet).ToLower().Replace("_", "-"); } }
    }


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

        public async Task<IEnumerable<Recipe>> SearchRecipes(string queryString, RecipeSearchSettings settings = null)
        {
            UriBuilder uri = new UriBuilder("https://api.edamam.com/search");
            uri.Query = $"app_id={appId}&app_key={appKey}&q={queryString}&from=0&to=50";

            if (settings != null)
            {
                if (settings.Time > 0)
                {
                    uri.Query += $"&time={settings.Time}";
                }

                if (settings.Excluded != null && settings.Excluded.Count() > 0)
                {
                    foreach(var excludedIngredient in settings.Excluded)
                    {
                        uri.Query += $"&excluded={excludedIngredient}";
                    }
                }

                if (settings.Health != null && settings.Health.Count() > 0)
                {
                    foreach(var healthSetting in settings.Health)
                    {
                        if (healthSetting == Health.DEFAULT)
                        {
                            continue;
                        }
                        uri.Query += $"&health={healthSetting.ToString().ToLower().Replace("_", "-")}";
                    }
                }

                uri.Query += $"&diet={settings.DietString}";
            }

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
