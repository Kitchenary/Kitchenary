using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Recommendation;
using EdamamService;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recommendation
{
    [TestClass]
    public class RecommenderTest
    {
        [TestMethod]
        public async Task Tests()
        {
            string[] foods = { "pork", "bread", "peppers", "sugar", "corn" };
            string[] pantryList = { "potatoes", "pork" };
            IngredientList userPreferences = new IngredientList(foods);
            IngredientList pantry = new IngredientList(pantryList);
            List<Tuple<Recipe, double>> recs = await Recommender.GetRecommendations(userPreferences, pantry);
            foreach (Tuple<Recipe, double> rec in recs)
            {
                Recipe rep = rec.Item1;
                Debug.WriteLine(rep.Label);
            }
        }
    }
}
