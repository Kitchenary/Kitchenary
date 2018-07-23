using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Math;
using Kitchenary.Classes;

namespace Kitchenary.Classes
{
    public class IngredientsOperations
    {

        private List<String> getTempVectorSpace(IngredientList listA, IngredientList listB)
        {
            return listA.Keys.Union(listB.Keys);
        }

        public double minSquaredDist(IngredientList listA, IngredientList listB)
        {
            double sum = 0;
            List<String> keys = getTempVectorSpace(listA, listB);
            foreach(String key in keys)
            {
                sum += Math.Pow(listA[key] - listB[key], 2);
            }

            return Math.Sqrt(sum);
        }

    }
}