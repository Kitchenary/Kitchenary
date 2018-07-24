using System;
using System.Collections.Generic;
using System.Linq;

namespace Recommendation
{
    public static class IngredientsOperations
    {

        private static IEnumerable<String> getTempVectorSpace(IngredientList listA, IngredientList listB)
        {
            return listA.Keys.Union(listB.Keys);
        }

        public static double MinSquaredDist(IngredientList listA, IngredientList listB)
        {
            double sum = 0;
            IEnumerable<String> keys = getTempVectorSpace(listA, listB);
            foreach(String key in keys)
            {
                sum += Math.Pow(((int) listA[key]) - ((int) listB[key]), 2);
            }

            return Math.Sqrt(sum);
        }

    }
}