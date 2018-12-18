using Microsoft.AspNet.Identity;
using SastoMithoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SastoMithoMVC.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
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
                else
                {
                    Guid Cartid = Guid.Parse(Request.Cookies["Cart"].Value);
                    var availablecart = await DataAccess.DataAccess.CheckCookieCart(Cartid);
                    if (!availablecart)
                    {
                        var result = await DataAccess.DataAccess.CreateCookieCart(Cartid, DateTime.Now.AddMonths(1));
                    }
                }
            }
            else
            {

                var availablecart = await DataAccess.DataAccess.CheckCart(Guid.Parse(User.Identity.GetUserId()));
                if (!availablecart)
                {
                    var result = await DataAccess.DataAccess.CreateCart(Guid.Parse(User.Identity.GetUserId()), Guid.NewGuid());
                }
            }
            CartViewModel cart = new CartViewModel();
            if (Request.IsAuthenticated)
            {
                
                cart.Items = await DataAccess.DataAccess.GetCartItems(Guid.Parse(User.Identity.GetUserId()));
            }
            else
            {
               
                Guid Cartid = Guid.Parse(Request.Cookies["Cart"].Value);
                cart.Items = await DataAccess.DataAccess.GetCookieCartItems(Cartid);
            }
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveItem()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
           
                return RedirectToAction("Index");
            
        }
    }
   
}
