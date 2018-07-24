using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kitchenary.Models;

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
            Models.PreferencesModel myModel = model;
            return View();
        }
    }
}