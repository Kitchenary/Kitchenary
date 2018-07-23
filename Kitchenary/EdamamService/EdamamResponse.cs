using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdamamService
{
    public class EdamamResponse
    {
        public IEnumerable<Hit> hits { get; set; }
    }

    public class Hit
    {
        public Recipe recipe { get; set; }
    }

    public class Recipe
    {
        public string Uri { get; set; }
        public string Label { get; set; }
        public string Image { get; set; }
        public string Source { get; set; }
        public string Url { get; set; }
        public float Yield { get; set; }
        public float Calories { get; set; }
        public float TotalWeight { get; set; }
        public IEnumerable<Ingredient> Ingredients { get; set; }

    }

    public class Ingredient
    {
        public string Text { get; set; }  
        public float Weight { get; set; }
    }
}
