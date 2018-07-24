using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kitchenary.Controllers
{
    public class RecommendationsController : Controller
    {
        // GET: Recommendations
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            return View();
        }
    }
}