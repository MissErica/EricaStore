using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EricaStore.Models;

namespace EricaStore.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            CartModel model = new CartModel();
            List<ProductsModel> cart = Session["Cart"] as List<ProductsModel>;

            if (cart == null)
            {
                cart =new List<ProductsModel>();
            }

            model.Items = new CartItemModel[cart.Count];
            model.Subtotal = 0;

            for (int i = 0; i < cart.Count; i++)
            {
                model.Items[i] = new CartItemModel
                {
                    Products = cart[i],
                    Quantity = 1
                };
                model.Subtotal += (model.Items[i].Products.Price) * (model.Items[i].Quantity ?? 0);

            }


            ViewBag.PageGenerationTime = DateTime.UtcNow;
           
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout()
        {
            return ViewBag.Message("You must be logged in!");
           RedirectToAction("MembershipInfo");
        }
    }
}