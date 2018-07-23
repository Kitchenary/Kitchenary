using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using EdamamService;
using Kitchenary.Models;

namespace Kitchenary.Controllers
{
    public class HomeController : Controller
    {
        private EdamamClient edamamClient;
        public HomeController()
        {
            edamamClient = new EdamamClient("b14df9ff", "46f071168e3ab576ca2144ea74109b37");
        }

        public async Task<ActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string query)
        {
            var result = await edamamClient.SearchRecipes(query);

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