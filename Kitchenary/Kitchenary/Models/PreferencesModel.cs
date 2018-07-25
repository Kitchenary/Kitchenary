using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kitchenary.Models
{
    public class PreferencesModel
    {
        public IEnumerable<EdamamService.Health> DietaryRestrictions { get; set; }
        public EdamamService.Diet Diet { get; set; }

        public IEnumerable<string> stuffIHate { get; set; }
    }
}