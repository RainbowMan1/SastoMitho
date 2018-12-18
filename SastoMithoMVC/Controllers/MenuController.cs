using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using SastoMithoClassLibrary.Models;
using SastoMithoMVC.DataAccess;
using SastoMithoMVC.Models;

namespace SastoMithoMVC.Controllers
{
    [AllowAnonymous]
    public class MenuController : Controller
    {
        // GET: Menu
        public async Task<ActionResult> Index()
        {
            if (!Request.IsAuthenticated)
            {
                if (Request.Cookies["Cart"] == null)
                {
                    string CartId = Guid.NewGuid().ToString();


                    HttpCookie cookie = new HttpCookie("Cart", CartId);
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.SetCookie(cookie);
                    var result = await DataAccess.DataAccess.CreateCookieCart(Guid.Parse(CartId), DateTime.Now.AddMonths(1));
                }
            }
            else
            {

                var availablecart = await DataAccess.DataAccess.CheckCart(Guid.Parse(User.Identity.GetUserId()));
                if (!availablecart) {
                    var result = await DataAccess.DataAccess.CreateCart(Guid.Parse(User.Identity.GetUserId()), Guid.NewGuid());
                }
            }

            List<CategoryModel> categories = new List<CategoryModel>();
            categories = await DataAccess.DataAccess.GetMenu();
            MenuModel menu = new MenuModel();
            menu.Menu = categories;
            return View(menu);
        }
        //public PartialViewResult AddtoCart(int Id)
        //{
        //    AddtoCartViewModel model = new AddtoCartViewModel { Id = Id };
        //    return PartialView(model);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddtoCart(AddtoCartViewModel model)
        {
            if (!ModelState.IsValid)
            {
               return  RedirectToAction("Index");
            }

            if (Request.IsAuthenticated) {
                var result = await DataAccess.DataAccess.UpdateCart(Guid.Parse(User.Identity.GetUserId()),model.Id,model.Quantity);
            }
            else
            {
                
                Guid Cartid = Guid.Parse(Request.Cookies["Cart"].Value);
                var result = await DataAccess.DataAccess.UpdateCookieCart(Cartid, model.Id, model.Quantity);
            }

            return RedirectToAction("Index");
        }
    }
}