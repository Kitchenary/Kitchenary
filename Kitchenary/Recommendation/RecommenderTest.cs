using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdamamService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Recommendation
{
    [TestClass]
    class RecommenderTest
    {
        [TestMethod]
        static async void Test(string[] args)
        {
            string[] foods = { "pork", "bread", "peppers", "sugar", "corn" };
            string[] pantryList = { "bread", "turkey", "corn", "potatoes", "cheese", "milk" };
            IngredientList userPreferences = new IngredientList(foods);
            IngredientList pantry = new IngredientList(pantryList);
            List<Tuple<Recipe, double>> recs = await Recommender.GetRecommendations(userPreferences, pantry);
            foreach (Tuple<Recipe, double> rec in recs)
            {
                Recipe rep = rec.Item1;
                Console.WriteLine(rep.Label);
            }
        }
    }
}
