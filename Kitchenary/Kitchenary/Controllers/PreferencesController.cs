using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchenary.Models;
using TableClients;
using Microsoft.WindowsAzure.Storage.Table;
using EdamamService;

namespace Kitchenary.Controllers
{
    public class PreferencesController : Controller
    {
        // GET: Preferences
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            var preferences = TableActions.GetPreferencesResult("PreferenceTable", userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value);
            var userPreference = preferences.FirstOrDefault();
            PreferencesModel model = new PreferencesModel();
            if (userPreference != null)
            {
                Diet diet;
                List<Health> dietaryRestrictions = new List<Health>();
                Enum.TryParse<Diet>(userPreference.dietPreference, out diet);

                IEnumerable<string> dietaryRestrictionsString = userPreference.healthPreference.Split(',');
                foreach(var dietaryRestrictionString in dietaryRestrictionsString)
                {
                    Health dietaryRestriction;
                    Enum.TryParse<Health>(dietaryRestrictionString, out dietaryRestriction);
                    dietaryRestrictions.Add(dietaryRestriction);
                }

                model.Diet = diet;
                model.DietaryRestrictions = dietaryRestrictions;
            }

            return View(model);
        }

        public ActionResult Create(PreferencesModel model)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            if (model.DietaryRestrictions == null)
            {
                model.DietaryRestrictions = new List<EdamamService.Health>();
            }

            PreferenceEntity preferences = new PreferenceEntity(userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value, userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value)
            {
                dietPreference = model.Diet.ToString(),
                healthPreference = string.Join(",", model.DietaryRestrictions.Select(x => x.ToString()))
            };

            TableActions.AddRow("PreferenceTable", (TableEntity)preferences);

            return View("Index", model);
        }
    }
}