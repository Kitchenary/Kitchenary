using System;
using System.Collections.Generic;
using System.Linq;

namespace Recommendation
{
    public class IngredientsOperations
    {

        private IEnumerable<String> getTempVectorSpace(IngredientList listA, IngredientList listB)
        {
            return listA.Keys.Union(listB.Keys);
        }

        public double minSquaredDist(IngredientList listA, IngredientList listB)
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