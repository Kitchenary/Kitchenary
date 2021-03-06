﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdamamService;

namespace Recommendation
{
    public static class Recommender
    {
        private static EdamamClient client;

        private static void InitializeClient()
        {
            client = new EdamamClient("b14df9ff", "46f071168e3ab576ca2144ea74109b37");
        }

        private static IEnumerable<String> getTempVectorSpace(IngredientList listA, IngredientList listB)
        {
            return listA.Keys.Union(listB.Keys);
        }

        public static double MinSquaredDist(IngredientList listA, double weightA, IngredientList listB, double weightB)
        {
            double sum = 0;
            IEnumerable<String> keys = getTempVectorSpace(listA, listB);
            foreach (String key in keys)
            {
                sum += Math.Pow(((int)listA[key])*weightA - ((int)listB[key])*weightB, 2);
            }

            return Math.Sqrt(sum);
        }

        private static List<Tuple<Recipe, double>> insertIntoRecipeList(Tuple<Recipe, double> addition, List<Tuple<Recipe, double>> list, int size)
        {
            int index = 0;
            foreach(Tuple<Recipe, double> recipe in list)
            {
                if(addition.Item2 < recipe.Item2)
                {
                    list.Insert(index, addition);
                    return list;
                }
                index++;
            }
            if (size < 12)
            {
                list.Add(addition);
            }

            return list;
        }

        public static List<Tuple<Recipe, double>> GetTopTwelveRecipes(IngredientList userPreferences, IEnumerable<Recipe> recipes)
        {
            List<Tuple<Recipe, double>> topTwelve = new List<Tuple<Recipe, double>>(13);
            double twelfthMin = 99999;
            int size = 0;

            foreach(Recipe recipe in recipes)
            {
                IngredientList recipeIngr = new IngredientList(recipe);
                double score = MinSquaredDist(userPreferences, 1.5, recipeIngr, 1);

                if (score < twelfthMin)
                {
                    topTwelve = insertIntoRecipeList(new Tuple<Recipe, double>(recipe, score), topTwelve, size);
                    size++;

                    if (size > 12)
                    {
                        twelfthMin = topTwelve.ElementAt(11).Item2;
                        topTwelve.RemoveAt(12);
                        size--;
                    } else if (size == 12)
                    {
                        twelfthMin = topTwelve.ElementAt(11).Item2;
                    }
                }
            }

            return topTwelve;
        }

        /* Steps:
         * Step One (when table objects are added): Convert into IngredientList objects
         * Step Two: Create a query with the items from the pantry and the allergy/diet restrictions
         * Step Three: Rank each recipe against the user preferences
         * Step Four: Return the sorted list
         */
        public static async Task<List<Tuple<Recipe, double>>> GetRecommendations(IngredientList userPreferences, IngredientList pantry, IEnumerable<string> dietaryRestrictions, string diet)
        {
            if(client == null)
            {
                InitializeClient();
            }

            //TODO: Set the RecipeSearchSettings with allergies and diets
            //Is there a way to figure out if the user settings have changed?

            String searchQuery = "";

            foreach(String key in pantry.Keys)
            {
                if(pantry[key].Equals(1))
                {
                    searchQuery += key + " ";
                }
            }

            RecipeSearchSettings searchSettings = new RecipeSearchSettings();
            if (dietaryRestrictions != null && dietaryRestrictions.Count() > 0)
            {
                foreach(var restriction in dietaryRestrictions)
                {
                    var enumRestriction = Health.DEFAULT;
                    Enum.TryParse<Health>(restriction, out enumRestriction);
                    if (searchSettings.Health == null)
                    {
                        searchSettings.Health = new List<Health>();
                    }
                    searchSettings.Health.Add(enumRestriction);
                }
            }

            if (diet != null)
            {
                var enumRestriction = Diet.BALANCED;
                Enum.TryParse<Diet>(diet, out enumRestriction);
                searchSettings.Diet = enumRestriction;
            }

            var recipeRecs = await client.SearchRecipes(searchQuery, searchSettings);

            if (recipeRecs.Count() == 0)
            {
                return new List<Tuple<Recipe, double>>();
            } else
            {
                return GetTopTwelveRecipes(userPreferences, recipeRecs);
            }
        }
        
    }
}
