using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EdamamService;

namespace Recommendation
{
    public class IngredientList : Dictionary<String, int>
    {

        public IngredientList(string[] foods) : base()
        {
            foreach(String food in foods)
            {
                this.Add(food, 1);
            }
        }

        String[] foods = { "beef", "bread", "turkey", "corn", "potatoes", "cheese", "milk", "pork", "peppers", "sugar", "limes", "lemons" };

        private String ExtractFood(String text)
        {
            text = text.Replace(",", "");
            String[] words = text.Split(' ');

            foreach(String word in words)
            {
                if (Array.IndexOf(foods, word) >= 0)
                {
                    return word;
                }
            }

            return null;
        }

        public IngredientList(Recipe recipe) : base()
        {
            IEnumerable<Ingredient> ingredients = recipe.Ingredients;
            foreach(Ingredient ingr in ingredients)
            {
                String food = ExtractFood(ingr.Text);
                if (food != null && !this.ContainsKey(food))
                {
                    this.Add(food, 1);
                }
            }
        }

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