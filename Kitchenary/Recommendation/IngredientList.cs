using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recommendation
{
    public class IngredientList : Dictionary<String, int>
    {

        //TODO: Make initializer for this from Excel table
        //TODO: Make initializer for this from the recipe thing

        public object this[String name]
        {
            get
            {
                if (this.ContainsKey(name))
                {
                    return base[name];
                } else
                {
                    return 0;
                }
            }
            set { base[name] = (int) value; }
        }

        public bool containsAll(IngredientList other)
        {
            foreach(String key in other.Keys)
            {
                if (other[key].Equals(1) && !this[key].Equals(1))
                {
                    return false;
                }
            }
            return true;
        }

    }
}