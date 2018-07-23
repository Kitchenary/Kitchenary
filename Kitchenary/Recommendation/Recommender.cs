using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdamamService;

namespace Recommendation
{
    static class Recommender
    {
        
        /* Steps:
         * Step One (when table objects are added): Convert into IngredientList objects
         * Step Two: Create a query with the items from the pantry and the allergy/diet restrictions
         * Step Three: Rank each recipe against the user preferences
         * Step Four: Return the sorted list
         */
        public static IEnumerable<Recipe> getRecommendations(IngredientList userPreferences, IngredientList pantry)
        {

        }
        
    }
}
