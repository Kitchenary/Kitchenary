using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchenary.Models;
using TableClients;
using Microsoft.WindowsAzure.Storage.Table;

namespace Kitchenary.Controllers
{
    public class PreferencesController : Controller
    {
        // GET: Preferences
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            return View();
        }

        public ActionResult Create(PreferencesModel model)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            if (model.DietaryRestrictions == null)
            {
                model.DietaryRestrictions = new List<EdamamService.Health>();
            }

            PreferenceEntity preferences = new PreferenceEntity(userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value, userClaims?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value)
            {
                dietPreference = model.Diet.ToString(),
                healthPreference = string.Join(",", model.DietaryRestrictions.Select(x => x.ToString()))
            };

            TableActions.AddRow("PreferenceTable", (TableEntity)preferences);

            return View("Index");
        }
    }
}