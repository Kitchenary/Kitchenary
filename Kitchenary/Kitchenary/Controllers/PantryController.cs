using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TableClients;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace Kitchenary.Controllers
{
    public class PantryModel
    {
        public string Item { get; set; }
        public string Quantity { get; set; }
    }
    public class PantryController : Controller
    {
        // GET: Pantry
        public ActionResult Index()
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            var results = TableActions.GetPantryResult("PantryTable", userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value);

            List<PantryModel> pantryItems = new List<PantryModel>();
            foreach (var item in results)
            {
                var pantryItem = item as PantryEntity;
                pantryItems.Add(new PantryModel { Item = pantryItem.RowKey, Quantity = pantryItem.quantity });
            }

            return View(pantryItems);
        }

        public ActionResult AddIngredient(string ingredient, string quantity)
        {
            var userClaims = User.Identity as System.Security.Claims.ClaimsIdentity;
            ViewBag.Name = userClaims?.FindFirst("name")?.Value;

            PantryEntity pantryEntity = new PantryEntity(userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value, ingredient);
            pantryEntity.foodCategory = "food";
            pantryEntity.quantity = quantity;
            pantryEntity.ExpirationTime = DateTime.UtcNow;
            TableActions.AddRow("PantryTable", (TableEntity)pantryEntity);

            var results = TableActions.GetPantryResult("PantryTable", userClaims?.FindFirst(System.IdentityModel.Claims.ClaimTypes.Name)?.Value);

            List<PantryModel> pantryItems = new List<PantryModel>();
            foreach(var item in results)
            {
                var pantryItem = item as PantryEntity;
                pantryItems.Add(new PantryModel { Item = pantryItem.RowKey, Quantity = pantryItem.quantity });
            }

            return View("Index", pantryItems);
        }
    }
}