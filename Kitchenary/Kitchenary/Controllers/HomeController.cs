using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using EdamamService;
using Kitchenary.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;

namespace Kitchenary.Controllers
{
    public class HomeController : Controller
    {
        private EdamamClient edamamClient;
        public HomeController()
        {
            edamamClient = new EdamamClient("b14df9ff", "46f071168e3ab576ca2144ea74109b37");
        }

        /// <summary>
        /// Send an OpenID Connect sign-in request.
        /// Alternatively, you can just decorate the SignIn method with the [Authorize] attribute
        /// </summary>
        public void SignIn()
        {
            if (!Request.IsAuthenticated)
            {
                HttpContext.GetOwinContext().Authentication.Challenge(
                    new AuthenticationProperties { RedirectUri = "/" },
                    OpenIdConnectAuthenticationDefaults.AuthenticationType);
            }
        }

        /// <summary>
        /// Send an OpenID Connect sign-out request.
        /// </summary>
        public void SignOut()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(
                OpenIdConnectAuthenticationDefaults.AuthenticationType,
                CookieAuthenticationDefaults.AuthenticationType);
        }

        public async Task<ActionResult> Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Index(string query)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            RecipeSearchSettings settings = new RecipeSearchSettings()
            {
                Time = 50,
                Diet = Diet.LOW_CARB,
                Excluded = new List<string> { "tofu" },
            };
            var result = await edamamClient.SearchRecipes(query, settings);

            return View(result);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
} 