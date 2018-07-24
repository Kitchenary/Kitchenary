using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Recommendation;
using EdamamService;
using System.Threading.Tasks;

namespace Kitchenary.Controllers
{
    public class RecommendationsController : Controller
    {
        // GET: Recommendations
        public async Task<ActionResult> Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            string[] foods = { "pork", "bread", "peppers", "sugar", "corn" };
            string[] pantryList = { "potatoes", "pork" };
            IngredientList userPreferences = new IngredientList(foods);
            IngredientList pantry = new IngredientList(pantryList);
            List<Tuple<Recipe, double>> recs = await Recommender.GetRecommendations(userPreferences, pantry);

            return View(recs);
        }
    }
}